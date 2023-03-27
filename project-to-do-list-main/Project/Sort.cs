using Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project
{
    class Sort
    {
        public class ToDoModelList : BindingList<ToDoModel>, IBindingList
        {
            private bool isSorted;
            private PropertyDescriptor sortProperty = null;
            private ListSortDirection sortDirection = ListSortDirection.Ascending;

            public bool SupportsSorting
            {
                get { return true; }
            }

            protected override bool SupportsSearchingCore
            {
                get { return true; }
            }

            public bool IsSorted
            {
                get { return isSorted; }
            }

            public PropertyDescriptor SortProperty
            {
                get { return sortProperty; }
            }

            public ListSortDirection SortDirection
            {
                get { return sortDirection; }
            }

            public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
            {
                sortProperty = property;
                sortDirection = direction;

                var items = this.Items as List<ToDoModel>;
                if (items != null)
                {
                    items.Sort(new SortComparer<ToDoModel>(property.Name, direction));
                    ResetBindings();
                    isSorted = true;
                }
                else
                {
                    isSorted = false;
                }
            }

        }

    }
}
