namespace WPF_Starter.Abstract
{
    internal class ViewModelBase: DevExpress.Mvvm.ViewModelBase
    {
        #region Properties

        #region Version

        public string ApplicationName => "$rootnamespace$";

        public string Title => $"{ApplicationName} v0";

        #endregion

        #endregion
    }
}
