namespace Chashavshavon.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        
        public Settings() {
            this.SettingsLoaded += new System.Configuration.SettingsLoadedEventHandler(Settings_SettingsLoaded);
            this.SettingsSaving += this.SettingsSavingEventHandler;
            
        }

        void Settings_SettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            if (!System.String.IsNullOrEmpty(Kavuahs))
            {
                var ser = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<Kavuah>));
                Kavuah.KavuahsList = (System.Collections.Generic.List<Kavuah>)ser.Deserialize(new System.IO.StringReader(Kavuahs));
            }
            else
            {
                Kavuah.KavuahsList = new System.Collections.Generic.List<Kavuah>();
            }
        }       
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            var ser = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<Kavuah>));
            var t = new System.IO.StringWriter();
            ser.Serialize(t, Kavuah.KavuahsList);
            Kavuahs = t.ToString();
            t.Dispose();            
        }
    }
}
