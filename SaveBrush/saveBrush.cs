/*
 * This file is part of SaveBrush.
 * SaveBrush is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * SaveBrush is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser Public License for more details.
 *
 * You should have received a copy of the GNU Lesser Public License
 * along with SaveBrush.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Text;
using NWN2Toolset;
using NWN2Toolset.Plugins;
using OEIShared;
using TD.SandBar;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

namespace SaveBrush 
    {

    public class MainSaveBrush : INWN2Plugin
        {

    private NWN2Toolset.NWN2.Views.NWN2TerrainEditorForm TE = NWN2Toolset.NWN2ToolsetMainForm.App.TerrainEditor;
    private string name = "Save Brush";
    private brushPreferences preferences = new brushPreferences();

    private MenuButtonItem m_cMenuItem;

    private void HandlePluginLaunch(object sender, EventArgs e)
        {
        brushSaveForm form = new brushSaveForm(preferences.filePath);
         form.ShowDialog();
        }

        public void Load(INWN2PluginHost cHost)
        { }

        private StringBuilder recNames(StringBuilder str, Control.ControlCollection con) {
             foreach (System.Windows.Forms.Control curCon in con) {
                 str.Append("\n" + curCon.Name + " ");
                 if (curCon.HasChildren) {
                     str.Append("Children{ ");
                     str = recNames(str, curCon.Controls);
                     str.Append("}");
                     }
                 }
             return str;
        }
        
        public void Shutdown(INWN2PluginHost cHost)
        {
        }

        public void Startup(INWN2PluginHost cHost)
        {
            m_cMenuItem = cHost.GetMenuForPlugin(this);
            m_cMenuItem.Activate += new EventHandler(this.HandlePluginLaunch);
            NWN2ToolsetMainForm.App.KeyPreview = true;
            NWN2ToolsetMainForm.App.KeyDown += new KeyEventHandler(NWN2BrushSaver);

        }

        public void NWN2BrushSaver(object sender, KeyEventArgs args)
            {
            brushSaveForm form = new brushSaveForm(preferences.filePath);
            if (preferences.Load.CompareTo(args.KeyData) == 0) {
               form.loadFile(null, null);
                } else 
                if (preferences.Save.CompareTo(args.KeyData) == 0)
                {
                form.saveFile(null, null);
                }
            }



        public void Unload(INWN2PluginHost cHost)
        {
        }

        public MenuButtonItem PluginMenuItem
        {
            get
            {
                return m_cMenuItem;
            }
        }

// Properties
        public string DisplayName
        {
            get
            {
                return name;
            }
        }

        public string MenuName
        {
            get
            {
                return name;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public object Preferences
        {
        get
            {
            return preferences;
            }
        set
            {
            preferences = (brushPreferences)value;
            }
        }
        }

    [Serializable]
    public class brushPreferences
        {
        DirectoryInfo directory = new DirectoryInfo("C:");
        Keys saveKey = Keys.Control | Keys.S;
        Keys loadKey = Keys.Control | Keys.L;

        [Category("Stored Brushes"), DisplayName("Directory"), Browsable(true), DefaultValue(typeof(DirectoryInfo), "C:"), Description("Location of the stored brushes")]
        public string filePath
            {
            get {
                String str = directory.FullName;
                if (Directory.Exists(str))
                    return str;
                else
                    return Directory.GetCurrentDirectory();
            }
            set { directory = new DirectoryInfo(value); }
            }

        [Category("Shortcuts"), Description("Shortcut to save brush"), Browsable(true), DefaultValue(typeof(Keys), "CTRL+S")]
        public Keys Save
            {
            get
                {
                return saveKey;
                }
            set
                {
                saveKey = value;
                }
            }

        [Category("Shortcuts"), Description("Shortcut to load brush."), Browsable(true), DefaultValue(typeof(Keys), "CTRL+L")]
        public Keys Load
            {
            get
                {
                return loadKey;
                }
            set
                {
                loadKey = value;
                }
            }


        }

    }

