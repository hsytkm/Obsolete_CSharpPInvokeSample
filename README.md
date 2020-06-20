## Move to

[hsytkm/PInvokeFromCSharp] (https://github.com/hsytkm/PInvokeFromCSharp)

## テク

DllImport に SuppressUnmanagedCodeSecurity を付与すると、セキュリティチェックが初めの１回しか行われなくなり高速化されるらしい。

構造体を渡す関数を100回コールしたが、効果がなかった。なぜ？

## 参考にさせていただいたページ


[【Windows/C#】なるべく丁寧にDllImportを使う] (https://qiita.com/mitsu_at3/items/94807ee0b3bf34ffb6b2)

[メンバに配列を持つ構造体のマーシャリング] (http://krdlab.hatenablog.com/entry/20061211/1165768253)

[CallingConvention Enum] (https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.interopservices.callingconvention?view=netframework-4.7.2)
