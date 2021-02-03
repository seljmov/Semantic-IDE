using System;
using System.Collections.Generic;
using Avalonia.Data;
using SemInterface.Models;
using SemInterface.Models.Tools;
using SemInterface.ViewModels;
using SemInterface.ViewModels.Tools;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Controls;

namespace SemInterface
{
    class MainDockFactory : Factory
    {
        private object _context;

        public MainDockFactory(object context)
        {
            _context = context;
        }

        public override IDock CreateLayout()
        {
            // - Подготовка панелей для объединения в layout
            // - Страница "Добро пожаловать"
            var welcome = new WelcomeViewModel
            {
                Id = "Welcome",
                Title = "Welcome"
            };
            
            // - Панель "Проект"
            var project = new ProjectViewModel
            {
                Id = "Project",
                Title = "Project"
            };
            
            // - Панель "Задачник"
            var taskbook = new TaskbookViewModel
            {
                Id = "Taskbook",
                Title = "Taskbook"
            };

            // - Панель "Документация"
            var help = new HelpViewModel
            {
                Id = "Help",
                Title = "Help"
            };

            // - Панель "Ошибки"
            var errors = new ErrorsViewModel
            {
                Id = "Errors",
                Title = "Errors"
            };

            // - Панель "Журнал команд"
            var commandLog = new CommandLogViewModel
            {
                Id = "CommandLog",
                Title = "CommandLog"
            };

            // - Панель "Консоль"
            var console = new ConsoleViewModel
            {
                Id = "Console",
                Title = "Console"
            };

            // - Делим layout на две части: нижнюю и верхнюю
            var mainLayout = new ProportionalDock
            {
                Id = "MainLayout",
                Title = "MainLayout",
                Proportion = double.NaN,
                Orientation = Orientation.Vertical,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    // - Верхняя часть содержит проект, задачник, докум-ю и редактор кода
                    new ProportionalDock
                    {
                        Id = "UpperPart",
                        Title = "UpperPart",
                        Proportion = double.NaN,
                        Orientation = Orientation.Horizontal,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new ToolDock
                            {
                                Id = "UpperTool1",
                                Title = "UpperTool1",
                                Proportion = double.NaN,
                                ActiveDockable = project,
                                VisibleDockables = CreateList<IDockable>
                                (
                                    project,    // - Вкладка со структурой проекта
                                    taskbook,   // - Вкладка с задачами
                                    help        // - Вкладка с документацией
                                )
                            },
                            new SplitterDock
                            {
                                Id = "UpperSplitter",
                                Title = "UpperSplitter"
                            },
                            new DocumentDock
                            {
                                Id = "DocumentPane",
                                Title = "DocumentPane",
                                Proportion = double.NaN,
                                ActiveDockable = welcome,
                                VisibleDockables = CreateList<IDockable>(welcome)
                            }
                        )
                    },
                    // - Разделитель частей
                    new SplitterDock
                    {
                        Id = "LayoutSplitter",
                        Title = "LayoutSplitter"
                    },
                    // - Нижняя часть содержит ошибки, журнал команд и консоль
                    new ProportionalDock
                    {
                        Id = "LowerPart",
                        Title = "LowerPart",
                        Proportion = double.NaN,
                        Orientation = Orientation.Horizontal,
                        ActiveDockable = null,
                        VisibleDockables = CreateList<IDockable>
                        (
                            new ToolDock
                            {
                                Id = "LowerTool1",
                                Title = "LowerTool1",
                                Proportion = double.NaN,
                                ActiveDockable = errors,
                                VisibleDockables = CreateList<IDockable>
                                (
                                    errors,
                                    commandLog
                                )
                            },
                            new SplitterDock
                            {
                                Id = "LowerSplitter",
                                Title = "LowerSplitter"
                            },
                            new ToolDock
                            {
                                Id = "LowerTool2",
                                Title = "LowerTool2",
                                Proportion = double.NaN,
                                ActiveDockable = console,
                                VisibleDockables = CreateList<IDockable>(console)
                            }
                        )
                    }
                )
            };

            var mainView = new MainViewModel
            {
                Id = "MainView",
                Title = "MainView",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            var root = CreateRootDock();

            root.Id = "Root";
            root.Title = "Root";
            root.ActiveDockable = mainView;
            root.DefaultDockable = mainView;
            root.VisibleDockables = CreateList<IDockable>(mainView);

            return root;
        }

        public override void InitLayout(IDockable layout)
        {
            this.ContextLocator = new Dictionary<string, Func<object>>
            {
                [nameof(IRootDock)] = () => _context,
                [nameof(IProportionalDock)] = () => _context,
                [nameof(IDocumentDock)] = () => _context,
                [nameof(IToolDock)] = () => _context,
                [nameof(ISplitterDock)] = () => _context,
                [nameof(IDockWindow)] = () => _context,
                [nameof(IDocument)] = () => _context,
                [nameof(ITool)] = () => _context,
                ["Welcome"] = () => new Welcome(),
                ["Project"] = () => new Project(),
                ["Taskbook"] = () => new Taskbook(),
                ["Help"] = () => new Help(),
                ["Errors"] = () => new Errors(),
                ["CommandLog"] = () => new CommandLog(),
                ["Console"] = () => new Consolee(),
                ["UpperPart"] = () => _context,
                ["UpperTool1"] = () => _context,
                ["UpperSplitter"] = () => _context,
                ["DocumentPane"] = () => _context,
                ["LayoutSplitter"] = () => _context,
                ["LowerPart"] = () => _context,
                ["LowerTool1"] = () => _context,
                ["LowerSplitter"] = () => _context,
                ["LowerTool2"] = () => _context,
                ["MainLayout"] = () => _context,
                ["Main"] = () => _context,
            };

            this.HostWindowLocator = new Dictionary<string, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () =>
                {
                    var hostWindow = new HostWindow()
                    {
                        [!HostWindow.TitleProperty] = new Binding("ActiveDockable.Title")
                    };
                    return hostWindow;
                }
            };

            this.DockableLocator = new Dictionary<string, Func<IDockable>>
            { };

            base.InitLayout(layout);
        }
    }
}