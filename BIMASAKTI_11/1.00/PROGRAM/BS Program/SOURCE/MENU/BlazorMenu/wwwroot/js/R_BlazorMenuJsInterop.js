// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

//export function showPrompt(message) {
//  return prompt(message, 'Type anything here');
//}

//export function selectText(tbId) {
//    var tb = document.querySelector("#" + tbId);

//    if (tb.select) {
//        tb.select();
//    }
//}

//export function setValueById(id, value) {
//    document.getElementById(id).value = value;
//}

//export function scrollToSelectedRow(gridSelector) {
//    var gridWrapper = document.querySelector(gridSelector);
//    if (gridWrapper) {
//        var selectedRow = gridWrapper.querySelector("tr.k-state-selected");
//        if (selectedRow) {
//            selectedRow.scrollIntoView();
//        }
//    }
//}

function showPrompt(message) {
    return prompt(message, 'Type anything here');
}

function selectText(tbId) {
    var tb = document.querySelector("#" + tbId);

    if (tb.select) {
        tb.select();
    }
}

function setValueById(id, value) {
    document.getElementById(id).value = value;
}

function scrollToSelectedRow(gridSelector) {
    var gridWrapper = document.querySelector(gridSelector);
    if (gridWrapper) {
        var selectedRow = gridWrapper.querySelector("tr.k-state-selected");
        if (selectedRow) {
            selectedRow.scrollIntoView();
        }
    }
}

//function encryptText(inputSrt, keyStr, ivStr) {

//    var keyBytes = aesjs.utils.utf8.toBytes(keyStr);
//    var ivBytes = aesjs.utils.utf8.toBytes(ivStr);
//    var textBytes = aesjs.utils.utf8.toBytes(inputSrt);

//    var aesCbc = new aesjs.ModeOfOperation.cbc(keyBytes, ivBytes);

//    var encryptedBytes = aesCbc.encrypt(aesjs.padding.pkcs7.pad(textBytes));
//    return aesjs.utils.hex.fromBytes(encryptedBytes);
//}

//function decryptText(inputStr, keyStr, ivStr) {
//    var keyBytes = aesjs.utils.utf8.toBytes(keyStr);
//    var ivBytes = aesjs.utils.utf8.toBytes(ivStr);
//    var encryptedBytes = aesjs.utils.hex.toBytes(inputStr);

//    var aesCbc = new aesjs.ModeOfOperation.cbc(keyBytes, ivBytes);

//    var decryptedBytes = aesCbc.decrypt(encryptedBytes);
//    return aesjs.utils.utf8.fromBytes(decryptedBytes);
//}