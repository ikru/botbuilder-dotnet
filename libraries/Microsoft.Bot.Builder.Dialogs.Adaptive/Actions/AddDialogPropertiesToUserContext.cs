// Licensed under the MIT License.
// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveExpressions.Properties;
using Newtonsoft.Json;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive.Actions
{
    public class AddDialogPropertiesToUserContext : Dialog
    {
        [JsonProperty("$kind")]
        public const string Kind = "Microsoft.AddDialogPropertiesToUserContext";

        [JsonConstructor]
        public AddDialogPropertiesToUserContext([CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0)
            : base()
        {
            this.RegisterSourceLocation(callerPath, callerLine);
        }

        /// <summary>
        /// Gets or sets an optional expression which if is true will disable this action.
        /// </summary>
        /// <example>
        /// "user.age > 18".
        /// </example>
        /// <value>
        /// A boolean expression. 
        /// </value>
        [JsonProperty("disabled")]
        public BoolExpression Disabled { get; set; }
        
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (options is CancellationToken)
            {
                throw new ArgumentException($"{nameof(options)} cannot be a cancellation token");
            }

            if (this.Disabled != null && this.Disabled.GetValue(dc.State) == true)
            {
                return await dc.EndDialogAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }

            foreach (var dialogProp in dc.ActiveDialog.State)
            {
                dc.State.TryGetValue($"{ScopePath.User}.previousContext.{dc.ActiveDialog.Id}.{dialogProp.Key}", out Stack<object> currentValue);

                if (currentValue == null)
                {
                    currentValue = new Stack<object>();
                }

                currentValue.Push(dialogProp.Value);

                dc.State.SetValue($"{ScopePath.User}.previousContext.{dc.ActiveDialog.Id}.{dialogProp.Key}", currentValue);
            }

            return await dc.EndDialogAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        protected override string OnComputeId()
        {
            return $"{this.GetType().Name}.addToUserContext";
        }
    }
}
