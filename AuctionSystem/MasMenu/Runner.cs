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
            _headMenu = MenuHendler.CreateNumberMenu(GetHeadMenuOptions());
            InitMenus();
        }
        public void Run()
        {
            _menuHendler.Runner.Run(_headMenu);
        }

        private void InitMenus()
        {
            _menuHendler.Runner.AddMenu("head", _headMenu);
        }
        private List<Option<int>> GetHeadMenuOptions()
        {
            var mainActions = new List<Option<int>>();
            mainActions.Add(new Option<int>(1, _controller.GetFutureAuctions, "for look at the next sells"));
            mainActions.Add(new Option<int>(2, _controller.GetCurrentAuctions, "for look at the auction that preforming right now"));
            mainActions.Add(new Option<int>(3, _controller.GetFinishedAuctions, "for look at the auction that have finished"));

            return mainActions;
        }

    }
}
