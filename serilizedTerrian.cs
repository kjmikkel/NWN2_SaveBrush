/*
 * This file is part of SaveBrush.
 * SaveBrush is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Multibrush is distributed in the hope that it will be useful,
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

public class serilizedTerrian
{
        float grassSize;
        float grassVariation;
        float innerRadius;
        float outerRadius;
        float refractBias;
        float refractPower;
        float rippleX;
        float rippleY;
        float scrollAngle1;
        float scrollAngle2;
            float scrollAngle3;
            
            float smoothness;
            int terrainBrushColor;
            string terrainBrushTexture;
            OEIShared.NetDisplay.TerrainModificationType terrainModificationType;
       
    float terrainValue;
            float terrainValue2;
            float textureMode;
    
    float scrollRate3;
            float scrollRate2;
    float scrollRate1;

            float scrollDirY3;
            float scrollDirY2;
    float scrollDirY1;

            float scrollDirX3;
            float scrollDirX2;
            float scrollDirX1;

           float terrainValue2;
           float textureMode;

           float ScrollRate3;
           float ScrollRate2;
           float ScrollRate1;

           float scrollDirY3;
        float scrollDirY2; 
           float scrollDirY1;

           float scrollDirX3;
           float ScrollDirX2;
           float ScrollDirX1;



    public serilizedTerrian()
	{
	}

    public void loadTerrian(NWN2Toolset.NWN2.Views.NWN2TerrainEditorForm TE) {
          float grassSize = TE.GrassSize;
            float grassVariation = TE.GrassVariation;
            float innerRadius = TE.InnerRadius;
            float outerRadius = TE.OuterRadius;
            float refractBias = TE.RefractBias;
            float refractPower = TE.RefractPower;
            float rippleX =  TE.RippleX;
            float rippleY = TE.RippleY;
            float scrollAngle1 = TE.ScrollAngle1;
            float scrollAngle2 = TE.ScrollAngle2;
            float scrollAngle3 = TE.ScrollAngle3;
            
            float smoothness = TE.Smoothness;
            int terrainBrushColor = TE.TerrainBrushColor;
            string terrainBrushTexture = TE.TerrainBrushTexture;
            OEIShared.NetDisplay.TerrainModificationType = TE.TerrainMode;
            float terrainValue = TE.TerrainValue;
           
        float terrainValue;
            float terrainValue2 =         TE.TerrainValue2;;
            float textureMode;
    
    float scrollRate3;
            float scrollRate2;
    float scrollRate1;

            float scrollDirY3;
            float scrollDirY2;
    float scrollDirY1;

            float scrollDirX3;
            float scrollDirX2;
            float scrollDirX1;



            TE.TerrainValue2;
            TE.TextureMode;
            TE.ScrollRate3;
            TE.ScrollRate2;
            TE.ScrollRate1;
            TE.ScrollDirY3;
            TE.ScrollDirY2;
            TE.ScrollDirY1;
            TE.ScrollDirX3;
            TE.ScrollDirX2;
            TE.ScrollDirX1;
        }
}
