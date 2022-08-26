using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace NftManagerABI.Contracts.NftManager.ContractDefinition
{
    public partial class Detail : DetailBase { }

    public class DetailBase
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("bool", "onSale", 2)]
        public virtual bool OnSale { get; set; }
        [Parameter("uint256", "price", 3)]
        public virtual BigInteger Price { get; set; }
        [Parameter("uint256", "category", 4)]
        public virtual BigInteger Category { get; set; }
        [Parameter("uint256", "tokenId", 5)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("address", "contractAddress", 6)]
        public virtual string ContractAddress { get; set; }
        [Parameter("string", "ipfsHash", 7)]
        public virtual string IpfsHash { get; set; }
    }
}
