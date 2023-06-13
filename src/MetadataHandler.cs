using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Landis.Library.Metadata;
using Landis.Core;
using Landis.Utilities;
using System.IO;
using Flel = Landis.Utilities;

namespace Landis.Extension.Output.BiomassByAge
{
    public static class MetadataHandler
    {
        public static ExtensionMetadata Extension { get; set; }

        public static void InitializeMetadata(int Timestep)
        {

            ScenarioReplicationMetadata scenRep = new ScenarioReplicationMetadata()
            {
                RasterOutCellArea = PlugIn.ModelCore.CellArea,
                TimeMin = PlugIn.ModelCore.StartTime,
                TimeMax = PlugIn.ModelCore.EndTime,
            };

            Extension = new ExtensionMetadata(PlugIn.ModelCore)
            //Extension = new ExtensionMetadata()
            {
                Name = PlugIn.ExtensionName,
                TimeInterval = Timestep, //change this to PlugIn.TimeStep for other extensions
                ScenarioReplicationMetadata = scenRep
            };

            //---------------------------------------            
            //          map outputs:         
            //---------------------------------------

            //OutputMetadata mapOut_BiomassRemoved = new OutputMetadata()
            //{
            //    Type = OutputType.Map,
            //    Name = "biomass removed",
            //    FilePath = @HarvestMapName,
            //    Map_DataType = MapDataType.Continuous,
            //    Map_Unit = FieldUnits.Mg_ha,
            //    Visualize = true,
            //};
            //Extension.OutputMetadatas.Add(mapOut_BiomassRemoved);

            foreach (ISpecies species in PlugIn.speciesToMap)
            {
                foreach(AgeClass age in PlugIn.ages[species.Name])
                {
                    OutputMetadata mapOut_Species = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = species.Name,
                        FilePath = PlugIn.MakeSpeciesMapName(species.Name,age.Name),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_Species);
                }
            }
            
            MetadataProvider mp = new MetadataProvider(Extension);
            mp.WriteMetadataToXMLFile("Metadata", Extension.Name, Extension.Name);
        }
        public static void CreateDirectory(string path)
        {
            //Require.ArgumentNotNull(path);
            path = path.Trim(null);
            if (path.Length == 0)
                throw new ArgumentException("path is empty or just whitespace");

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir))
            {
                Flel.Directory.EnsureExists(dir);
            }

            //return new StreamWriter(path);
            return;
        }
    }
}