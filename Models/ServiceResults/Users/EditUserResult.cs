using Microsoft.AspNetCore.Identity;
using Models.Entities.Identity;
using System.Collections.Generic;

namespace Models.ServiceResults.Users
{
    /// <summary>
    /// A class that represents the result of the edit user operations.
    /// </summary>
    public class EditUserResult
    {
        #region Fields

        private IEnumerable<IdentityError> _errors;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation has succeeded.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        public IEnumerable<IdentityError> Errors
        {
            get
            {
                _errors = _errors ?? new List<IdentityError>();
                return _errors;
            }
            set
            {
                _errors = value;
            }
        }

        #endregion
    }
}
