using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace NftManagerABI.Contracts.NftManager.ContractDefinition
{


    public partial class NftManagerDeployment : NftManagerDeploymentBase
    {
        public NftManagerDeployment() : base(BYTECODE) { }
        public NftManagerDeployment(string byteCode) : base(byteCode) { }
    }

    public class NftManagerDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x";
        public NftManagerDeploymentBase() : base(BYTECODE) { }
        public NftManagerDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class CloseSaleFunction : CloseSaleFunctionBase { }

    [Function("closeSale")]
    public class CloseSaleFunctionBase : FunctionMessage
    {
        [Parameter("address", "contractAddress", 1)]
        public virtual string ContractAddress { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class EstablishFunction : EstablishFunctionBase { }

    [Function("establish")]
    public class EstablishFunctionBase : FunctionMessage
    {
        [Parameter("address", "contractAddress", 1)]
        public virtual string ContractAddress { get; set; }
        [Parameter("uint256", "category", 2)]
        public virtual BigInteger Category { get; set; }
        [Parameter("uint256", "num", 3)]
        public virtual BigInteger Num { get; set; }
        [Parameter("string", "ipfsHash", 4)]
        public virtual string IpfsHash { get; set; }
    }

    public partial class GetInfoFunction : GetInfoFunctionBase { }

    [Function("getInfo", typeof(GetInfoOutputDTO))]
    public class GetInfoFunctionBase : FunctionMessage
    {
        [Parameter("address", "contractAddress", 1)]
        public virtual string ContractAddress { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class GetNFTsFunction : GetNFTsFunctionBase { }

    [Function("getNFTs", typeof(GetNFTsOutputDTO))]
    public class GetNFTsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "category", 1)]
        public virtual BigInteger Category { get; set; }
    }

    public partial class NftsFunction : NftsFunctionBase { }

    [Function("nfts", typeof(NftsOutputDTO))]
    public class NftsFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("uint256", "", 2)]
        public virtual BigInteger ReturnValue2 { get; set; }
    }

    public partial class PurchaseFunction : PurchaseFunctionBase { }

    [Function("purchase")]
    public class PurchaseFunctionBase : FunctionMessage
    {
        [Parameter("address", "contractAddress", 1)]
        public virtual string ContractAddress { get; set; }
        [Parameter("uint256", "tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class StartSaleFunction : StartSaleFunctionBase { }

    [Function("startSale")]
    public class StartSaleFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "price", 1)]
        public virtual BigInteger Price { get; set; }
        [Parameter("address", "contractAddress", 2)]
        public virtual string ContractAddress { get; set; }
        [Parameter("uint256", "tokenId", 3)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class TestforWorkingFunction : TestforWorkingFunctionBase { }

    [Function("testforWorking", "uint256")]
    public class TestforWorkingFunctionBase : FunctionMessage
    {

    }





    public partial class GetInfoOutputDTO : GetInfoOutputDTOBase { }

    [FunctionOutput]
    public class GetInfoOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("tuple", "", 1)]
        public virtual Detail ReturnValue1 { get; set; }
    }

    public partial class GetNFTsOutputDTO : GetNFTsOutputDTOBase { }

    [FunctionOutput]
    public class GetNFTsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("tuple[]", "", 1)]
        public virtual List<Detail> ReturnValue1 { get; set; }
    }

    public partial class NftsOutputDTO : NftsOutputDTOBase { }

    [FunctionOutput]
    public class NftsOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("bool", "onSale", 2)]
        public virtual bool OnSale { get; set; }
        [Parameter("uint256", "price", 3)]
        public virtual BigInteger Price { get; set; }
        [Parameter("bool", "valid", 4)]
        public virtual bool Valid { get; set; }
    }





    public partial class TestforWorkingOutputDTO : TestforWorkingOutputDTOBase { }

    [FunctionOutput]
    public class TestforWorkingOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
