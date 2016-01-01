# Cli-wallet: command-line Ripple wallet in .NET

This repository contains the following projects:

* CliWallet - proof-of-concept command-line Ripple wallet (partially implemented)
* CliWallet.RippleRest - Ripple-REST client library (partially implemented)
* CliWallet.Tests - some unit tests for CliWallet
* CliWallet.RippleRest.Tests - unit, integration and acceptance tests for Ripple-REST client.

## Warning
Very important: [Ripple-REST development has stopped](https://github.com/ripple/ripple-rest).

## Configuration
See file wallet.config (app.config before compilation).

## Examples

### Help
```
c:\wallet.exe help
Known commands (some are not implemented):
getaccountsettings
getbalance
getorders
gettransactions
gettrustlines
getversion
help
send
signandsubmit
```

### Account settings

```
c:\wallet.exe getaccountsettings
Account                 rEhKZcz5Ndjm9BzZmmKrtvhXPnSWByssDv
TransactionSequence     10064
DefaultRipple           False
DisableMaster           False
DisallowXrp             False
Domain
EmailHash
GlobalFreeze            False
NoFreeze                False
RequireAuthorization    False
RequireDestinationTag   False
TransferRate
```

### Settings of another account

```
c:\wallet.exe getaccountsettings rJnZ4YHCUsHvQu7R6mZohevKJDHFzVD6Zr
Account                 rJnZ4YHCUsHvQu7R6mZohevKJDHFzVD6Zr
TransactionSequence     1134574
DefaultRipple           False
DisableMaster           False
DisallowXrp             False
Domain
EmailHash
GlobalFreeze            False
NoFreeze                False
RequireAuthorization    False
RequireDestinationTag   False
TransferRate
```

### Wallet balance

```
c:\wallet.exe getbalance
  2381182.83    XRP
           0    USD     r9vbV3EHvXWjSkeQ6CAcYVPGeq7TuiXY2X
    20000.57    USD     rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B
           0    BTC     rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B
           0    USD     rDk49t24s9QHAASSfkrMzTkTTpEBY4jBnU
           1    STR     rJHygWcTLVpSXkowott6kzgZU6viQSVYM1
```

### Balance of another account

```
c:\wallet.exe getbalance rJnZ4YHCUsHvQu7R6mZohevKJDHFzVD6Zr
  1266508.99    XRP
     1792.26    USD     rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B
           0    015841551A748AD2C1F76FF6ECB0CCCD00000000        rs9M85karFkCRjvc6KMWn8Coigm9cbcgcx
           0    0158415500000000C1F76FF6ECB0BAC600000000        rrh7rf1gV2pXAoqA8oYbpHd8TKv5ZQeo67
           0    JPY     rMAz5ZnK73nyNUL4foAvaxdreczCkG3vA6
      605.68    EUR     rUU8vXA3p6d92skbArWjaKWqfbtxyrr65W
           0    JPY     r94s8px6kSw1uZ1MV98dhSRTvc6VMPoPcN
        1.65    XAU     rrh7rf1gV2pXAoqA8oYbpHd8TKv5ZQeo67
           0    BTC     rMwjYedjc7qqtKYVLiAccJSmCwih4LnE2q
           0    EUR     rMwjYedjc7qqtKYVLiAccJSmCwih4LnE2q
           0    USD     rMwjYedjc7qqtKYVLiAccJSmCwih4LnE2q
       15.42    BTC     rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B
    49899.51    MXN     rG6FZ31hDHN1K5Dkbma3PSB5uVCuVVRzfn
        4.37    BTC     rG6FZ31hDHN1K5Dkbma3PSB5uVCuVVRzfn
           0    USD     rDhJYbUDE763aqeTbjc34yqJSYHvykSKV5
```

### Send XRPs

```
c:\wallet.exe send 10+XRP rHugAv6dMm37Z2m1cC4TeboFsa6LKXRdzh
Preparing payment...
Submitting payment...
Done, Ripple-REST returned 'tesSUCCESS'
```
