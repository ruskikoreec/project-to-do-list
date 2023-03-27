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
            private PropertyDescriptor sortProperty = null;
            private ListSortDirection sortDirection = ListSortDirection.Ascending;

            public PropertyDescriptor SortProperty
            {
                get { return sortProperty; }
            }

            public ListSortDirection SortDirection
            {
                get { return sortDirection; }
            }

           
        }

    }
}
