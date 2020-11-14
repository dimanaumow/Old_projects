﻿using System;
using System.Globalization;
using System.IO;
using System.Text;
using FileCabinetApp.CommandArgHandlers;
using FileCabinetApp.CommandHandlers;
using FileCabinetApp.CommandHandlers.ConcreteServiceHandlers;
using FileCabinetApp.Printers;
using FileCabinetApp.Service;
using FileCabinetApp.Validators;

namespace FileCabinetApp
{
    /// <summary>
    /// Provide work with file cabinet service.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Dzmitry Naumov";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private static IFileCabinetService fileCabinetService;
        private static bool isRunning = true;

        /// <summary>
        /// Start point for application.
        /// </summary>
        /// <param name="args">Array of command-line keys passed to the program.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            var handler = new Handler();
            handler.Handle(args);
            fileCabinetService = handler.GetService();
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var inputs = Console.ReadLine().Split(' ', 2);
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                var commandHandlers = CreateCommandHandlers();

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                const int parametersIndex = 1;
                string parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                commandHandlers.Handle(new AppCommandRequest(command, parameters));
                Console.WriteLine();
            }
            while (isRunning);
        }

        public static ICommandHandler CreateCommandHandlers()
        {
            static void Runner(bool x) => isRunning = x;

            var printer = new SimplePrinter();
            var createHandler = new CreateCommandHandler(fileCabinetService);
            var exitHandler = new ExitCommandHandler(Runner);
            var exportHandler = new ExportCommandHandler(fileCabinetService);
            var findHandler = new FindCommandHandler(fileCabinetService, printer.Print);
            var importHandler = new ImportCommandHandler(fileCabinetService);
            var listHandler = new ListCommandHandler(fileCabinetService, printer.Print);
            var helpHandler = new HelpCommandHandler();
            var purgeHandler = new PurgeCommandHandler(fileCabinetService);
            var statHandler = new StatCommandHandler(fileCabinetService);
            var removeHandler = new RemoveCommandHandler(fileCabinetService);
            var editHandler = new EditCommandHandler(fileCabinetService);

            createHandler.SetNext(exitHandler);
            exitHandler.SetNext(exportHandler);
            exportHandler.SetNext(findHandler);
            findHandler.SetNext(importHandler);
            importHandler.SetNext(listHandler);
            listHandler.SetNext(helpHandler);
            helpHandler.SetNext(purgeHandler);
            purgeHandler.SetNext(statHandler);
            statHandler.SetNext(removeHandler);
            removeHandler.SetNext(editHandler);

            return createHandler;
        }
    }
}
