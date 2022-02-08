using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EFSresources.EFSlibrary
{
    public class EFScollections
    {
        public static ObservableCollection<T> ListToObservableCollection<T>(IEnumerable<T> list)
        {
            var oc = new ObservableCollection<T>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    oc.Add(item);
                }
            }

            return oc;
        }
    }
}
