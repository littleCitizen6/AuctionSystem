using MenuBuilder.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasMenu
{
    public class Runner
    {
        MenuHendler _menuHendler;
        MasController _controller;
        IMenu _headMenu;
        public Runner(MasController controller)
        {
            _controller = controller;
            _menuHendler = new MenuHendler();
       //     _headMenu = MenuHendler.CreateNumberMenu(GetHeadMenuOptions());
        }
        public void Run()
        {
            _menuHendler.Runner.Run(_headMenu);
        }

        private void InitMenus()
        {
           // _menuHendler.Runner.AddMenu("1", _menuHendler.CreateStringsFunctionMenu(_controller.MissleFactory.GetMissles(), _controller.AddMissle));
            //_menuHendler.Runner.AddMenu("head", _headMenu);
            //_menuHendler.Runner.AddMenu("2", _menuHendler.CreateStringsFunctionMenu(GetFunc2MenuContent(), Launch));//ToDo: make this menu get TotalWar option and implement function
        }
       /* private List<Option<int>> GetHeadMenuOptions()
        {
            var mainActions = new List<Option<int>>();
            mainActions.Add(new Option<int>(1, _menuHendler.MoveToOtherMenu, "choose missle to add"));
            mainActions.Add(new Option<int>(2, _menuHendler.MoveToOtherMenu, "choose missle to launch"));
            mainActions.Add(new Option<int>(3, _controller.StockReport, "get stock report"));
            mainActions.Add(new Option<int>(4, Remove, "remove missle"));
            mainActions.Add(new Option<int>(5, _menuHendler.Exit, "exit from shayetet6"));
            return mainActions;
        }*/

    }
}
