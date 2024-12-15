using PalMathy.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PalMathy.ViewModels
{
    class NavigatorViewModel : BaseViewModel
    {
        private Page _page = new MainPage();
        public Page CurrentPage
        {
            get
            {
                return _page;
            }
            set
            {
                Set(ref _page, value);
            }
        }

        public ICommand GoToMenu
        {
            get
            {
                return new DelegateCommand((obj) => {
                    CurrentPage = new MainPage();                    
                });
            }
        }

        public ICommand GoToFunctionModule
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CurrentPage = new DihotomyPage();
                });
            }
        }


        public ICommand GoToDefinitiveIntegral
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CurrentPage = new DefinitiveIntegralPage();
                });
            }
        }

        public ICommand GoToSortingsModule
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    CurrentPage = new SortingPage();
                });
            }
        }
    }
}
