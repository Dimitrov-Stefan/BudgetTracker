﻿using Microsoft.AspNetCore.Identity;
using Models.Entities.Identity;
using System.Collections.Generic;

namespace Steffes.Web.Models.Account
{
    /// <summary>
    /// A class that represents the result of the create account operations.
    /// </summary>
    public class CreateAccountResult
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