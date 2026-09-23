#nullable enable
namespace StarViewerMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        // MainPage = new AppShell(); // 削除: 非推奨
    }

    /// <summary>
    /// アプリケーションのルート <see cref="Window"/> を作成します。
    /// </summary>
    /// <param name="activationState">
    /// 起動またはアクティベーション時の状態を表す <see cref="IActivationState"/>。  
    /// プラットフォームや復元シナリオに応じた情報が含まれる場合があります。null を許容します。
    /// </param>
    /// <returns>
    /// ルートページ（このアプリケーションでは <see cref="AppShell"/> を含む）を配置した <see cref="Window"/> を返します。
    /// </returns>
    /// <remarks>
    /// .NET MAUI ではウィンドウをオーバーライドしてカスタム初期化や複数ウィンドウ対応を行います。  
    /// 必要に応じてここでプラットフォーム固有の初期化やウィンドウ設定を追加してください。
    /// </remarks>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        // ここでルートページを含む Window を作成する
        return new Window(new AppShell());
    }
}