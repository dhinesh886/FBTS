using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace FBTS.Model.Common.XmlObjectModel
{
    public class Settings
    {
        [XmlElement("Setting")]
        public List<Setting> SettingsList = new List<Setting>();

        public string PageTitle
        {
            get { return GetSettingsValue("PageTitle"); }
        } 
        public string Type
        {
            get { return GetSettingsValue("Type"); }
        }
        public string Mode
        {
            get { return GetSettingsValue("Mode"); }
        }
        public string Parent
        {
            get { return GetSettingsValue("Parent"); }
        }
        public string Sub
        {
            get { return GetSettingsValue("Sub"); }
        }
        public string DataViewId
        {
            get { return GetSettingsValue("DVID"); }
        }

        public string GetSettingsValue(string argsKey)
        {
            var value = string.Empty;
            if (SettingsList != null && SettingsList.Count > 0)
            {
                value = SettingsList.Where(x => String.Equals(x.Name, argsKey, StringComparison.CurrentCultureIgnoreCase)).Select(y => y.Value).FirstOrDefault();
            }
            return value;
        }
    }
}
