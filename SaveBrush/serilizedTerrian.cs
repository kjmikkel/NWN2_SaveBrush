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
using OEIShared;
using NWN2Toolset;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

[Serializable]
public class serializableTerrain
        {
    private string name;
    public Hashtable table;
    public int[] grass;
    public string[] textures = new string[6];
    public Color color;

    public serializableTerrain(String name)
        {
        this.name = name;
        table = new Hashtable();
        }
    }
