mergeInto(LibraryManager.library, {

    // 0 : connected
    // 1 : window.etheruem not defined
    // 2 : wallet not metamask
    // 3 : wrong chain id
ConnectToMetamask: function () {
        if (window.ethereum === 'undefined') {
            return 1;
        }
        if (!window.ethereum.isMetaMask) {
            return 2;
        }
        if (window.ethereum.chainId !== "0x4") {
            return 3;
        }
        new Promise(function (resolve, reject) {
            var result = window.ethereum.enable();
            console.log("connecting to Metamask: " + result);
        }).then(function (response) { console.log("Metamask connected it seems " + response); });
        return 0;
},

GetAccount: function () {
        var account = '';
        if (typeof window.ethereum !== 'undefined' && window.ethereum.isMetaMask) {
            account = window.ethereum.selectedAddress;
            if (typeof account === 'undefined' || account == null) {
                account = '';
            }
        }
        var buffer = _malloc(lengthBytesUTF8(account) + 1);
        stringToUTF8(account, buffer, account.length + 1);
        return buffer;
},

ChangePermissionTransaction: function (_landAbi, _landContractAddress, _nftTraderContractAddress, _perm) {
    var landAbi = Pointer_stringify(_landAbi);
    var nftTraderContractAddress = Pointer_stringify(_nftTraderContractAddress);
    var landContractAddress = Pointer_stringify(_landContractAddress);
    web3 = new Web3(window.ethereum);
    if (typeof web3 !== 'undefined') {
        var abi = JSON.parse(landAbi);
        var myContract = new web3.eth.Contract(abi, landContractAddress);

        new Promise(function (resolve, reject) {
            myContract.methods
                .setApprovalForAll(nftTraderContractAddress, _perm)
                .send({
                    from: window.web3.currentProvider.selectedAddress,
                    to: landContractAddress,
                })
                .on('error', function (error) {
                    console.log("error" + error);
                })
                .on('transactionHash', function (transactionHash) {
                    console.log("hash" + transactionHash);
                });
        }).then(function (response) { console.log(response); });
    }
    else {
        console.log("web3 not defined");
    }
},

BuyTransaction: function (_nftAbi, _nftTraderContractAddress, _landContractAddress, _landId, _amount) {
    var nftAbi = Pointer_stringify(_nftAbi);
    var nftTraderContractAddress = Pointer_stringify(_nftTraderContractAddress);
    var landContractAddress = Pointer_stringify(_landContractAddress);
    var landId = Pointer_stringify(_landId);
    var amount = Pointer_stringify(_amount);
    web3 = new Web3(window.ethereum);
    if (typeof web3 !== 'undefined') {
        var abi = JSON.parse(nftAbi);
        var myContract = new web3.eth.Contract(abi, nftTraderContractAddress);

        new Promise(function (resolve, reject) {
            myContract.methods
                .purchase(landContractAddress, landId)
                .send({
                    from: window.web3.currentProvider.selectedAddress,
                    to: nftTraderContractAddress,
                    value: amount
                })
                .on('error', function (error) {
                    console.log("error" + error);
                })
                .on('transactionHash', function (transactionHash) {
                    console.log("hash" + transactionHash);
                });
            }).then(function (response) { console.log(response); });
        }
        else {
            console.log("web3 not defined");
        }
  },

StartSaleTransaction: function (_nftAbi, _nftTraderContractAddress, _landContractAddress, _landId, _amount) {
    var nftAbi = Pointer_stringify(_nftAbi);
    var nftTraderContractAddress = Pointer_stringify(_nftTraderContractAddress);
    var landContractAddress = Pointer_stringify(_landContractAddress);
    var landId = Pointer_stringify(_landId);
    var amount = Pointer_stringify(_amount);
    web3 = new Web3(window.ethereum);
    if (typeof web3 !== 'undefined') {
        var abi = JSON.parse(nftAbi);
        console.log(abi);
        var myContract = new web3.eth.Contract(abi, nftTraderContractAddress);
        new Promise(function (resolve, reject) {
            myContract.methods
                .startSale(amount, landContractAddress, landId)
                .send({
                    from: window.web3.currentProvider.selectedAddress,
                    to: nftTraderContractAddress,
                })
                .on('error', function (error) {
                    console.log("error" + error);
                })
                .on('transactionHash', function (transactionHash) {
                    console.log("hash" + transactionHash);
                })
                .on('receipt', function (receipt) {
                    console.log("receipt" + receipt.contractAddress);
                })
                .on('confirmation', function (confirmationNumber, receipt) {
                    console.log("confirmed");
                });
        }).then(function (response) { console.log(response); });
    }
    else {
        console.log("web3 not defined");
    }
},


CloseSaleTransaction: function (_nftAbi, _nftTraderContractAddress, _landContractAddress, _landId) {
    var nftAbi = Pointer_stringify(_nftAbi);
    var nftTraderContractAddress = Pointer_stringify(_nftTraderContractAddress);
    var landContractAddress = Pointer_stringify(_landContractAddress);
    var landId = Pointer_stringify(_landId);
    web3 = new Web3(window.ethereum);
    if (typeof web3 !== 'undefined') {
        var abi = JSON.parse(nftAbi);
        var myContract = new web3.eth.Contract(abi, nftTraderContractAddress);
        new Promise(function (resolve, reject) {
            myContract.methods
                .closeSale(landContractAddress, landId)
                .send({
                    from: window.web3.currentProvider.selectedAddress,
                    to: nftTraderContractAddress,
                })
                .on('error', function (error) {
                    console.log("error" + error);
                })
                .on('transactionHash', function (transactionHash) {
                    console.log("hash" + transactionHash);
                })
                .on('receipt', function (receipt) {
                    console.log("receipt" + receipt.contractAddress);
                })
                .on('confirmation', function (confirmationNumber, receipt) {
                    console.log("confirmed");
                });
        }).then(function (response) { console.log(response); });
    }
    else {
        console.log("web3 not defined");
    }
},

BuildTransaction: function (_landAbi, _landContractAddress, _landId) {
    var landAbi = Pointer_stringify(_landAbi);
    var landContractAddress = Pointer_stringify(_landContractAddress);
    var landId = Pointer_stringify(_landId);
    web3 = new Web3(window.ethereum);
    if (typeof web3 !== 'undefined') {
        var abi = JSON.parse(landAbi);
        var myContract = new web3.eth.Contract(abi, landContractAddress);
        new Promise(function (resolve, reject) {
            myContract.methods
                .build(landId)
                .send({
                    from: window.web3.currentProvider.selectedAddress,
                    to: landContractAddress,
                })
                .on('error', function (error) {
                    console.log("error" + error);
                })
                .on('transactionHash', function (transactionHash) {
                    console.log("hash" + transactionHash);
                })
                .on('receipt', function (receipt) {
                    console.log("receipt" + receipt.contractAddress);
                })
                .on('confirmation', function (confirmationNumber, receipt) {
                    console.log("confirmed");
                });
        }).then(function (response) { console.log(response); });
    }
    else {
        console.log("web3 not defined");
    }
}

  
});
