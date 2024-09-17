using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

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


    // Efficiently add many items to the ObservableCollection. UI will be less laggy.
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }

}
