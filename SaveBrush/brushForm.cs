﻿/*
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using NWN2Toolset.NWN2.Views;

namespace SaveBrush
    {
    [Serializable]
    public struct terrainInfo
        {
        public int barPressure, barOuter, barFourth, barInner, bladeSize, bladeVeriation;

        public terrainInfo(int barPressure, int barOuter, int barFourth, int barInner)
            {
            this.barPressure = barPressure;
            this.barOuter = barOuter;
            this.barFourth = barFourth;
            this.barInner = barInner;
            bladeSize = 0;
            bladeVeriation = 0;
            }

        public terrainInfo(int barPressure, int barOuter, int barFourth, int barInner, int bladeSize, int bladeVeriation)
            {
            this.barPressure = barPressure;
            this.barOuter = barOuter;
            this.barFourth = barFourth;
            this.barInner = barInner;
            this.bladeSize = bladeSize;
            this.bladeVeriation = bladeVeriation;
            }
        }

    public partial class brushSaveForm : Form
        {
        private NWN2Toolset.NWN2.Views.NWN2TerrainEditorForm TE = NWN2Toolset.NWN2ToolsetMainForm.App.TerrainEditor;

        private RadioButton opTerrain;
        private RadioButton opGrass;
        private RadioButton opTexture;
        private RadioButton opWater;

        private RadioButton raiseControl;
        private RadioButton lowerControl;
        private RadioButton noiseControl;
        private RadioButton smoothControl;
        private RadioButton flattenControl;
        private RadioButton colorControl;
        private RadioButton walkControl;
        private RadioButton nowalkControl;
        
        // To restore the view the user had before
        private RadioButton OPRadio;
        private RadioButton under;

        private Label[] labelTextures = new Label[6] { null, null, null, null, null, null };
        private RadioButton[] radioTextures = new RadioButton[6] { null, null, null, null, null, null };

        private ListBox listGrass;
        private ListBox listTexture;

        private TrackBar barPressure;
        private TrackBar barOuter;
        private TrackBar barInner;
        private TrackBar barFourth;
        private Button colorButton;

        private TrackBar blade;
        private TrackBar variation;

        private String filePath;

        Hashtable table;
        String filter = "Brush  files (*.bru)|*.bru";

        public brushSaveForm(String filePath)
            {
            InitializeComponent();

            this.filePath = filePath;
            #region Controls
            opTerrain = (RadioButton)findControl("radioButtonElevation", TE.Controls);
            opTexture = (RadioButton)findControl("radioButtonTexture", TE.Controls);
            opGrass = (RadioButton)findControl("radioButtonPaintGrass", TE.Controls);
            opWater = (RadioButton)findControl("radioButtonPaintWater", TE.Controls);

            raiseControl = (RadioButton)findControl("radioButtonRaise", TE.Controls);
            colorControl = (RadioButton)findControl("radioButtonColor", TE.Controls);
            flattenControl = (RadioButton)findControl("radioButtonFlatten", TE.Controls);
            smoothControl = (RadioButton)findControl("radioButtonSmooth", TE.Controls);
            noiseControl = (RadioButton)findControl("radioButtonNoise", TE.Controls);
            walkControl = (RadioButton)findControl("radioButtonWalk", TE.Controls);
            lowerControl = (RadioButton)findControl("radioButtonLower", TE.Controls);
            nowalkControl = (RadioButton)findControl("radioButtonNoWalk", TE.Controls);



            barPressure = (TrackBar)findControl("trackBarValue", TE.Controls);
            barOuter = (TrackBar)findControl("trackBarOuterRadius", TE.Controls);
            barInner = (TrackBar)findControl("trackBarInnerRadius", TE.Controls);
            barFourth = (TrackBar)findControl("trackBarFourth", TE.Controls);

            blade = (TrackBar)findControl("trackBarGrassSize", TE.Controls);
            variation = (TrackBar)findControl("trackBarGrassVariation", TE.Controls);

            colorButton = (Button)findControl("buttonColor", TE.Controls);

            listGrass = (ListBox)findControl("listBoxGrass", TE.Controls);
            listTexture = (ListBox)findControl("listBoxTextureNames", TE.Controls);

            for (int i = 0; i < 6; i++)
                {
                labelTextures[i] = (Label)findControl("labelRadioButtonTexture" + (i + 1).ToString(), TE.Controls);
                radioTextures[i] = (RadioButton)findControl("radioButtonTexture" + (i + 1).ToString(), TE.Controls);
                }

            table = new Hashtable();
            table.Add("OPTerrain", opTerrain);
            table.Add("radioButtonRaise", raiseControl);
            table.Add("radioButtonColor", colorControl);
            table.Add("radioButtonFlatten", flattenControl);
            table.Add("radioButtonSmooth", smoothControl);
            table.Add("radioButtonNoise", noiseControl);
            table.Add("radioButtonWalk", walkControl);
            table.Add("radioButtonLower", lowerControl);
            table.Add("radioButtonNoWalk", nowalkControl);
            table.Add("OPtexture", opTexture);
            table.Add("OPgrass", opGrass);
            table.Add("OPwater",opWater);
            #endregion
           

            Text = "Save Brush: Load or save brush presets";
            
            }

        // Find out which pane the user has selected
        private void findSelections()
            {
            RadioButton but;
            int count = 0;
            foreach (String key in table.Keys)
                {
                but = (RadioButton)table[key];
                if (but.Checked)
                    {
                    if (key.StartsWith("OP"))
                        {
                        OPRadio = but;
                        count++;
                        }
                    else
                        {
                        under = but;
                        count++;
                        }
                    }
                if (count == 2) break;
                }
            }

        // Restore the settings to what they were before 
        private void restoreSelection()
            {
            if (OPRadio != null)
                {
                OPRadio.Checked = true;

                if (under != null)
                    {
                    under.Checked = true;
                    }
                }
            }

        private void quit(object sender, System.EventArgs e)
            {
            this.Close();
            }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
            {
            Assembly ayResult = null;
            string sShortAssemblyName = null;
            if (args != null)
                {
                sShortAssemblyName = args.Name.Split(',')[0];
                }

            Assembly[] ayAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly ayAssembly in ayAssemblies)
                {
                if (sShortAssemblyName == ayAssembly.FullName.Split(',')[0])
                    {
                    ayResult = ayAssembly;
                    break;
                    }
                }
            return ayResult;
            }

        public void loadFile(object sender, System.EventArgs e)
            {
            OpenFileDialog load = new OpenFileDialog();
            load.Filter = filter;
            if (filePath != null && filePath != "")
                load.InitialDirectory = filePath;
            
            if (load.ShowDialog() == DialogResult.OK)
                {
                String fileName = load.FileName;
                ResolveEventHandler resolveEventHandler = new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                AppDomain.CurrentDomain.AssemblyResolve += resolveEventHandler;

                FileStream file = new FileStream(fileName, FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();

                Object temp = bin.Deserialize(file);
                serializableTerrain ter = temp as serializableTerrain;
                file.Close();
                loadControls(ter);
                }
            this.Close();
            }

        public void saveFile(object sender, System.EventArgs e)
            {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = filter;
            if (filePath != null && filePath != "")
                save.InitialDirectory = filePath;

            if (save.ShowDialog() == DialogResult.OK)
                {
                String fileName = save.FileName;
                FileInfo info = new FileInfo(fileName);
                serializableTerrain tar = saveControls(info.Name);

                FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                var formater = new BinaryFormatter();
                formater.Serialize(fileStream, tar);
                fileStream.Close();
                }
            this.Close();
            }

        private void loadControls(serializableTerrain ter)
            {
            findSelections();
            RadioButton but;
            terrainInfo info;
            opTerrain.Checked = true;
            foreach (String key in table.Keys)
                {
                but = (RadioButton)table[key];
                but.Checked = true;
                info = (terrainInfo)ter.table[key];
                barPressure.Value = info.barPressure;
                barOuter.Value = info.barOuter;
                barInner.Value = info.barInner;
                barFourth.Value = info.barFourth;
                variation.Value = info.bladeVeriation;
                blade.Value = info.bladeSize;
            /*
                if (key == "texture")
                    {
                    for (int i = 0; i < 6; i++)
                        {
                        labelTextures[i].Text = ter.textures[i];
                        labelTextures[i].BackColor = ter.images[i];
                        }
                    }
             * */
                }

            colorButton.BackColor = ter.color;
            
            int count = listGrass.Items.Count;
            for (int i = 0; i < count; i++)
                {
                listGrass.SetSelected(i, false);
                }

            foreach (int i in ter.grass)
                {
                listGrass.SetSelected(i, true);
                }
            restoreSelection();
            }

        public serializableTerrain saveControls(String name)
            {
            serializableTerrain ter = new serializableTerrain(name);
            RadioButton but;
            terrainInfo info;

            findSelections();
            foreach (String key in table.Keys)
                {
                but = (RadioButton)table[key];
                but.Checked = true;
                info = new terrainInfo(barPressure.Value, barOuter.Value, barFourth.Value, barInner.Value, blade.Value, variation.Value);
                ter.table.Add(key, info);
               /*
                if (key == "texture")
                    {
                    for (int i = 0; i < 6; i++)
                        {
                        ter.textures[i] = labelTextures[i].Text;
                        //                        Console.WriteLine(ter.textures[i]);
                        //                      Console.WriteLine(TE.GetTexture(i));
                        ter.images[i] = labelTextures[i].BackColor;
                        }
                    
                    //          radioTextures[1].Text = "TT_GD_Dirt_01";
                    //         radioTextures[1].BackgroundImage = bitMapReturn(radioTextures[1].Text);
                    //     labelTextures[1].Text = stringReturn(radioTextures[1].Text);
                    //    Console.WriteLine("Txture: " + radioTextures[1].Text + ", label: " + labelTextures[1].Text);
                    //   Console.WriteLine("name: " + radioTextures[1].BackgroundImage);
                    //                    radioTextures[1].Checked = true;
                    }
            */
                
                }
            ter.color = colorButton.BackColor;
            var l = listGrass.SelectedIndices;
            int[] intArray = new int[l.Count];
            
            int j = 0;
            foreach (int i in l)
                {
                intArray[j] = i;
                j++;
                }
            ter.grass = intArray;
            restoreSelection();
            return ter;
            }

        private Bitmap bitMapReturn(string str)
            {
            foreach (TextureListItem item in listTexture.Items)
                {
                if (item.GetImageName() == str)
                    {
                    return item.Image;
                    }

                }
            return null;
            }

        private string stringReturn(string str)
            {
                {
                foreach (TextureListItem item in listTexture.Items)
                    {
                    if (item.GetImageName() == str)
                        {
                        return item.ToString();
                        }
                    }
                return null;
                }
            }

        private static System.Windows.Forms.Control findControl(String name, System.Windows.Forms.Control.ControlCollection controls)
            {
            System.Windows.Forms.Control result;
            foreach (System.Windows.Forms.Control newCur in controls)
                {
                if (newCur.Name == name) return newCur;
                if (newCur.HasChildren)
                    {
                    result = findControl(name, newCur.Controls);
                    if (result != null) return result;
                    }
                }
            return null;
            }

        }
    }
