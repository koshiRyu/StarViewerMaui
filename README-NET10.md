# StarViewerMaui .NET 10 更新版

元の ZIP のソースを基に、対象フレームワークを net10.0-android / ios / maccatalyst / windows に更新しました。Android の古い、互いに矛盾する署名設定を削除し、Microsoft.Maui.Controls への明示的な参照を追加しました。AASharp 2.12.0 は元のままです。旧 myapp.keystore はパスワード不明のため含めていません。

## Visual Studio 2026 での確認

1. .NET MAUI 開発ワークロードと .NET 10 SDK、Android SDK をインストール。
2. StarViewerMaui.sln を開いて Android を選択。まず Debug ビルドで問題を確認。
3. エラーがあれば NuGet の AASharp 依存関係とアプリのコードを確認。更新前の net6.0 用コードがそのまま通る保証はありません。
4. Release で発行する際に新しい署名キーストアを作成し、APK または AAB を選択。鍵・エイリアス・パスワードを安全に保管してください。

この作業環境には dotnet SDK と Android SDK がなく、復元・コンパイル・署名・端末実行は未検証です。この ZIP は署名済み APK/AAB ではありません。新しい鍵で署名したアプリは、旧鍵によるインストールを通常は上書き更新できません。
