using System.Collections.Generic;
using System.IO;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Actions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Conditions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Generators;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Recognizers;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Templates;
using Microsoft.Bot.Builder.LanguageGeneration;

namespace Microsoft.BotBuilderSamples
{
    public class TestDialog : ComponentDialog
    {
        private Templates _lgFile;

        public TestDialog()
            : base(nameof(TestDialog))
        {
            _lgFile = Templates.ParseFile(Path.Join(".", "Dialogs", "TestDialog", "TestDialog.lg"));
            var testDialog = new AdaptiveDialog("rootDialog")
            {
                AutoEndDialog = false,
                Generator = new TemplateEngineLanguageGenerator(),
                Recognizer = new RegexRecognizer()
                {
                    Intents = new List<IntentPattern>()
                    {
                        new IntentPattern()
                        {
                            Intent = "why",
                            Pattern = "why"
                        }
                    }
                },
                Triggers = new List<OnCondition>()
                {
                    new OnBeginDialog()
                    {
                        Actions = new List<Dialog>()
                        {
                            new TextInput()
                            {
                                Id = "askForName",
                                Prompt = new ActivityTemplate("What is your name?"),
                                Property = "user.name"
                            },
                            new SendActivity("I have ${user.name}")
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "why",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity()
                            {
                                Id = "Self",
                                Activity = new ActivityTemplate("I have ${join(dialogContext.stack, ' -> ')}")
                            },
                            new IfCondition()
                            {
                                Condition = "dialogContext.activeDialog == 'askForName'",
                                Actions = new List<Dialog>()
                                {
                                    new SendActivity("I need your name to complete the sample")
                                },
                                ElseActions = new List<Dialog>()
                                {
                                    new SendActivity("I just need the info..")
                                }
                            }
                        }
                    }
                }
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(testDialog);

            // The initial child dialog to run.
            InitialDialogId = "rootDialog";
        }
    }
}
