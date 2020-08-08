﻿// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Bot.Schema
{

    /// <summary>
    /// Defines values for EndOfConversationCodes.
    /// </summary>
    public static class EndOfConversationCodes
    {
        /// <summary>
        /// The code value for unknown end of conversations.
        /// </summary>
        public const string Unknown = "unknown";

        /// <summary>
        /// The code value for successful end of conversations.
        /// </summary>
        public const string CompletedSuccessfully = "completedSuccessfully";

        /// <summary>
        /// The code value for user cancelled end of conversations.
        /// </summary>
        public const string UserCancelled = "userCancelled";

        /// <summary>
        /// The code value for bot time out end of conversations.
        /// </summary>
        public const string BotTimedOut = "botTimedOut";

        /// <summary>
        /// The code value for bot-issued invalid message end of conversations.
        /// </summary>
        public const string BotIssuedInvalidMessage = "botIssuedInvalidMessage";

        /// <summary>
        /// The code value for channel failed end of conversations.
        /// </summary>
        public const string ChannelFailed = "channelFailed";
    }
}
