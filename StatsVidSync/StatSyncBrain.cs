using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace StatsVidSync
{
    public static class StatSyncBrain
    {
        public static string FinishedXml;
        public static string FcpXmlFilePath;
        public static string SoloStatsXmlFilePath;
        private static string KeywordValue { get; set; }
        public static string KeywordPlaybackDuration { get; set; }
        public static decimal startInPointAdjustmentMs { get; set; }


        private static XDocument _fcpDoc;
        private static XDocument _statsDoc;
        private static readonly Dictionary<string, int> SetStartTimesDict = new Dictionary<string, int>();


        // Load xml documents
        public static void ProcessXmlFiles(string fcpDocPath, string statsDocPath)
        {
            _fcpDoc = XDocument.Load(FcpXmlFilePath);
            _statsDoc = XDocument.Load(SoloStatsXmlFilePath);

            RemoveNamespaces(_statsDoc);

            CreateSetStartDictionary(_fcpDoc);

            ProcessMoments(_statsDoc, _fcpDoc);

            RemoveSmartCollections(_fcpDoc);

            FinishedXml = _fcpDoc.ToString();

            //fcpDoc.Save(@"C:\XML2XSD\finished.xml");

        }

        public static void SetKeywordDurationValues(string duration)
        {
            KeywordPlaybackDuration = duration + "s";
            decimal.TryParse(duration, out var bufferOffset);
            startInPointAdjustmentMs = (bufferOffset / 2) * 1000;
        }


        private static void RemoveSmartCollections(XDocument fcpDoc)
        {
            fcpDoc.Descendants("smart-collection").Remove();
        }

        private static void ProcessMoments(XDocument statsDoc, XDocument fcpDoc)
        {
            //1 get the type of moment
            //2 if beginMatch do nothing.
            //3 if beginSet, setup time offset
            //4 if skill, set up keywords for given skill

            var moments = GetNodeCollection(statsDoc, "moment");

            decimal offsetMilliseconds = 0;


            foreach (XElement moment in moments)
            {
                string keywordValue = "";
                string momentType = GetMomentType(moment);

                // for now, we will ignore these moment types.  We could use scoreAdjustment to create score overlay titles in FCP.
                if (momentType == "beginMatch" || momentType == "endMatch" || momentType == "endSet" || momentType == "scoreAdjustment" || momentType == "error")
                    continue;

                if (momentType == "beginSet")
                    GetOffsetMillisecondsForSet(moment, out offsetMilliseconds);

                if (momentType == "serve")
                    CreateKeywordsServe(moment, out keywordValue);

                if (momentType == "dig")
                    CreateKeywordsDig(moment, out keywordValue);

                if (momentType == "attack")
                    CreateKeywordsAttack(moment, out keywordValue);

                if (momentType == "block")
                    CreateKeywordsBlock(moment, out keywordValue);

                if (momentType == "freeBall")
                    CreateKeywordsFreeBall(moment, out keywordValue);

                if (momentType == "pass")
                    CreateKeywordsPass(moment, out keywordValue);

                if (momentType == "set")
                    CreateKeywordsSet(moment, out keywordValue);

                var keywordStart = CalculateKeywordBeginMilliseconds(offsetMilliseconds, moment);

                KeywordValue = keywordValue;

                //AdjustKeywordStartSeconds(keywordStart, 0, out string fcpKeywordStartTime);

                fcpDoc.Descendants("asset-clip").FirstOrDefault()
                                               .Add(new XElement("keyword",
                                                new XAttribute("start", keywordStart),
                                                new XAttribute("duration", KeywordPlaybackDuration),
                                                new XAttribute("value", KeywordValue)));

            }

            //foreach (var moment in moments)
            //{

            //    XMLUtilities.keywordValue = "";

            //    var tags = moment.Descendants("tag");

            //    if (tags.Descendants("value").Any(v => v.Value == "beginSet"))
            //        GetOffsetMillisecondsForSet(moment, out offsetMilliseconds);

            //    decimal.TryParse(moment.Element("startTimeMs").Value, out decimal momentStartTimeMs);
            //    decimal keyStartMs = (offsetMilliseconds + momentStartTimeMs);

            //    if (keyStartMs > 0 || keyStartMs < 0)
            //    {
            //        keywordStart = ((keyStartMs - XMLUtilities.StartPlaybackBufferMSeconds) / 1000).ToString() + "s";
            //    }
            //    else if (keyStartMs - XMLUtilities.StartPlaybackBufferMSeconds > 0)
            //    {
            //        keywordStart = (keyStartMs - XMLUtilities.StartPlaybackBufferMSeconds).ToString() + "s";

            //    }
            //    else
            //    {
            //        keywordStart = "0s";
            //    }

            //    foreach (var tag in tags)
            //    {
            //        XMLUtilities.keywordValue = XMLUtilities.keywordValue + tag.Element("key").Value + "-" + tag.Element("value").Value + ", ";
            //    }

            //    fcpDoc.Descendants("asset-clip").FirstOrDefault()
            //                                   .Add(new XElement("keyword", 
            //                                    new XAttribute("start", keywordStart), 
            //                                    new XAttribute("duration", XMLUtilities.KeywordPlaybackDuration), 
            //                                    new XAttribute("value", XMLUtilities.keywordValue)));                

            //}


        }



        private static string CalculateKeywordBeginMilliseconds(decimal offsetMilliseconds, XElement moment)
        {
            string keywordStart;
            decimal.TryParse(moment.Element("startTimeMs")?.Value, out decimal momentStartTimeMs);
            decimal keyStartMs = (offsetMilliseconds + momentStartTimeMs);

           if (keyStartMs - startInPointAdjustmentMs > 0)
            {
                keywordStart = ((keyStartMs - startInPointAdjustmentMs) / 1000).ToString(CultureInfo.InvariantCulture) + "s";

            }
            else
            {
                keywordStart = "0s";
            }

            return keywordStart;
        }


        private static void CreateKeywordsServe(XElement moment, out string keywordValue)
        {
            string action = "";
            string quality = GetTagKeyValue(moment, "quality");
            string result = GetTagKeyValue(moment, "result");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (result == null)
                action =  "serve-" + quality;

            if (result == "ace")
                action = "ace";

            if (result == "error")
                action = "serve err";

            if (playerJersey == null)
            {
                playerJersey = "opp";
            }

            string kw = string.Join(", ", action + " - player-" + playerJersey, "rot-" + rotation, "player-" + playerJersey, action, "serve");

            keywordValue = kw;
        }

        private static void CreateKeywordsDig(XElement moment, out string keywordValue)
        {
            string action = "";
            string result = GetTagKeyValue(moment, "result");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (result == null)
                action = "dig";

            if (result == "error")
                action = "dig err";

            if (playerJersey == null)
            {
                playerJersey = "opp";
            }


            string kw = string.Join(", ", action + " - player-" + playerJersey, "rot-" + rotation, "player-" + playerJersey, action);

            keywordValue = kw;
        }

        private static void CreateKeywordsAttack(XElement moment, out string keywordValue)
        {
            string action = "";
            string result = GetTagKeyValue(moment, "result");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (result == null)
                action = "attack";

            if (result == "kill")
                action = "attack-kill";

            if (result == "error")
                action = "attack-err";

            if (playerJersey == null)
            {
                playerJersey = "opp";
            }

            string kw = string.Join(", ", action + " - player-" + playerJersey, "rot-" + rotation, "player-" + playerJersey, action);

            keywordValue = kw;
        }

        private static void CreateKeywordsBlock(XElement moment, out string keywordValue)
        {
            string action = "";
            string result = GetTagKeyValue(moment, "result");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (result == null)
                action = "block attempt";

            if (result == "block")
                action = "block";

            if (result == "error")
                action = "block err";

            if (playerJersey == null)
            {
                playerJersey = "opp";
            }

            string kw = string.Join(", ", action + "-player -" + playerJersey, "rot-" + rotation, "player-" + playerJersey, action);

            keywordValue = kw;
        }

        private static void CreateKeywordsFreeBall(XElement moment, out string keywordValue)
        {
            string quality = GetTagKeyValue(moment, "quality");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (playerJersey == null)
            {
                playerJersey = "opp";
            }

            string kw = string.Join(", ", "freeball-" + quality + " - player-"  + playerJersey, "rot-" + rotation, "player-" + playerJersey, "freeball-" + quality);

            keywordValue = kw;

        }

        private static void CreateKeywordsPass(XElement moment, out string keywordValue)
        {
            string quality = GetTagKeyValue(moment, "quality");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (playerJersey == null)
            {
                playerJersey = "opp";
            }

            string kw = string.Join(", ", "pass-" + quality + " - player-" + playerJersey, "rot-" + rotation, "player-" + playerJersey, "pass-" + quality);

            keywordValue = kw;

        }

        private static void CreateKeywordsSet(XElement moment, out string keywordValue)
        {
            string result = GetTagKeyValue(moment, "result");
            string rotation = GetTagKeyValue(moment, "rotation");
            string playerJersey = GetTagKeyValue(moment, "playerJersey");

            if (playerJersey == null)
            {
                playerJersey = "zz";
            }

            string kw = string.Join(", ", result + " - player-" + playerJersey, "rot-" + rotation, "player-" + playerJersey, result);

            keywordValue = kw;

        }


        private static string GetTagKeyValue(XElement moment, string tagKeyName)
        {
            string tagValue = (from item in moment.Descendants("tag")
                               where item.Descendants("key").First().Value == tagKeyName
                               select item.Element("value")?.Value).FirstOrDefault();

            return tagValue;
        }

        private static string GetMomentType(XElement moment)
        {
            var tag = moment.Descendants("tag").FirstOrDefault();

            string momentType = tag.Element("value").Value;

            //from t in tags
            //          where t.Descendants("key").Any(k => k.Value == "type")
            //          select t.Descendants("value").Single().Value.ToString();

            return momentType;


        }

        private static void GetOffsetMillisecondsForSet(XElement moment, out decimal offsetMilliseconds)
        {
            decimal.TryParse(moment.Element("startTimeMs")?.Value, out decimal originalStartTime);
            decimal dictSetStart = GetStartTimeForSet();

            offsetMilliseconds = dictSetStart - originalStartTime;
        }


        private static decimal GetStartTimeForSet()
        {
            decimal setStartTime = 0;

            if (SetStartTimesDict.TryGetValue("Set 1 Start", out var dictSetStart))
                {
                	SetStartTimesDict.Remove("Set 1 Start");
                    setStartTime = dictSetStart;
                    return setStartTime;
                }


            if (SetStartTimesDict.TryGetValue("Set 2 Start", out dictSetStart))
            {
                SetStartTimesDict.Remove("Set 2 Start");
                setStartTime = dictSetStart;
                return setStartTime;
            }

            if (SetStartTimesDict.TryGetValue("Set 3 Start", out dictSetStart))
            {
                SetStartTimesDict.Remove("Set 3 Start");
                setStartTime = dictSetStart;
                return setStartTime;
            }

            if (SetStartTimesDict.TryGetValue("Set 4 Start", out dictSetStart))
            {
                SetStartTimesDict.Remove("Set 4 Start");
                setStartTime = dictSetStart;
                return setStartTime;
            }

            if (SetStartTimesDict.TryGetValue("Set 5 Start", out dictSetStart))
            {
                SetStartTimesDict.Remove("Set 5 Start");
                setStartTime = dictSetStart;
                return setStartTime;
            }


            return setStartTime;
        }

        private static IEnumerable<XElement> GetNodeCollection(XDocument doc, string rootDescendantXName, string childDescendantXName, string childDescendantValue)
        {

            var collectionObject = from c in doc.Root?.Descendants(rootDescendantXName)
                                   where c.Descendants(childDescendantXName).Any(x => x.Value == childDescendantValue)
                                   select c;

            return collectionObject;

        }

        private static IEnumerable<XElement> GetNodeCollection(XDocument doc, string rootDescendantXName, string childDescendantXName)
        {

            var collectionObject = from c in doc.Root?.Descendants(rootDescendantXName)
                                   where c.Descendants(childDescendantXName).Any()
                                   select c;

            return collectionObject;

        }

        private static IEnumerable<XElement> GetNodeCollection(XDocument doc, string rootDescendantXName)
        {

            var collectionObject = from c in doc.Root?.Descendants(rootDescendantXName)
                                   select c;

            return collectionObject;

        }


        private static void RemoveNamespaces(XDocument document)
        {
            var elements = document.Descendants();
            elements.Attributes().Where(a => a.IsNamespaceDeclaration).Remove();
            foreach (var element in elements)
            {
                element.Name = element.Name.LocalName;

                var strippedAttributes =
                    from originalAttribute in element.Attributes().ToArray()
                    select (object)new XAttribute(originalAttribute.Name.LocalName, originalAttribute.Value);

                //Note that this also strips the attributes' line number information
                element.ReplaceAttributes(strippedAttributes.ToArray());
            }
        }

        private static void CreateSetStartDictionary(XDocument fcpDoc)
        {
            if (fcpDoc.Root != null)
            {
                if (SetStartTimesDict.Count > 0)
                {
                    SetStartTimesDict.Clear();
                }
                var markerTimes = from m in fcpDoc.Root.Descendants("marker")
                    select new
                    {
                        markerValue = m.Attribute("value")?.Value,
                        markerStart = CalculateStartTime(m.Attribute("start")?.Value)

                    };
                foreach (var markerTime in markerTimes)
                {
                    SetStartTimesDict.Add(markerTime.markerValue, markerTime.markerStart);
                }
            }
        }

        private static int CalculateStartTime(string value)
        {
            int setStartMilliseconds;

            string startTime = value.Replace("s", "");

            if (startTime != "0")
            {
                decimal.TryParse(startTime.Split('/')[0], out var numerator);
                decimal.TryParse(startTime.Split('/')[1], out var denominator);
                setStartMilliseconds = (int)(Math.Round(numerator / denominator, 2) * 1000);
            }
            else
            {
                setStartMilliseconds = 0;
            }



            return setStartMilliseconds;
        }
    }
}



        
       

       
    