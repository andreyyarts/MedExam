using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Common.Interactivity
{
    /// <summary>
    /// Interface used by the <see cref="PopupWindowAction"/>.
    /// If the DataContext object of a view that is shown with this action implements this interface
    /// it will be populated with the <see cref="Notification"/> data of the interaction request 
    /// as well as an <see cref="Action"/> to finish the request upon invocation.
    /// </summary>
    public interface IInteractionRequestAware
    {
        /// <summary>
        /// The <see cref="Notification"/> passed when the interaction request was raised.
        /// </summary>
        Notification Notification { get; set; }

        /// <summary>
        /// An <see cref="Action"/> that can be invoked to finish the interaction.
        /// </summary>
        Action FinishInteraction { get; set; }
    }
}
