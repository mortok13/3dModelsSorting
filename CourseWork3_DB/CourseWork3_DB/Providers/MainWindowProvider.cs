using System;

namespace CourseWork3_DB
{
    public class MainWindowProvider
    {
        public static MainWindowProvider Instance { get; } = new MainWindowProvider();

        private MainWindowProvider()
        {
        }

        private MainWindow _windowInstance;
        public MainWindow Window => _windowInstance ?? throw new InvalidOperationException("There is no window.");
        public void Initialize(MainWindow window)
        {
            _windowInstance = window;
        }
    }
}