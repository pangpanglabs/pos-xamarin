# pos-xamarin
Pangpang POS Client using Xamarin.Forms.

## How to use PosSDK

Get PosSDK Service:
```C#
PosSDK PosSDK => DependencyService.Get<PosSDK>();
```

Call API
```C#
var result = await PosSDK.CallAPI<Account>("/account/login", new
{
    tenant = this.Tenant,
    username = this.Username,
    password = this.Password,
});
if (result.Success == true)
{
    /* success */
}
else
{
    /* fail */
{
```
