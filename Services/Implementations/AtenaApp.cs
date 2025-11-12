using System;
using Atena.Services.Interfaces;
using Gtk;

namespace Atena.Services.Implementations;

public class AtenaApp : IAtenaApp
{
    private readonly MainWindow _mainWindow;

    public AtenaApp(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
    
    public void RunApp()
    {
        
        Application.Init();

        // var app = new Application("org.Atena.Atena", GLib.ApplicationFlags.None);
        // app.Register(GLib.Cancellable.Current);

        // var win = 
        // app.AddWindow(win);

        // win.Show();

        _mainWindow.ShowAll();

        Application.Run();
    }
}
