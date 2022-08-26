using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace LandABI.Contracts.Land.ContractDefinition
{
    public partial class Metadata : MetadataBase { }

    public class MetadataBase
    {
        [Parameter("string", "name", 1)]
        public virtual string Name { get; set; }
        [Parameter("uint256", "category", 2)]
        public virtual BigInteger Category { get; set; }
        [Parameter("string", "description", 3)]
        public virtual string Description { get; set; }
    }
}
