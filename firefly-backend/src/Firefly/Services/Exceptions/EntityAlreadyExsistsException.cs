using System;

namespace Firefly.Services.Exceptions
{
    /// <summary>
    /// Indicates that an entity already exists.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EntityAlreadyExsistsException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExsistsException"/> class.
        /// </summary>
        public EntityAlreadyExsistsException() : base("An entity already exists.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExsistsException"/> class.
        /// </summary>
        /// <param name="id">The ID of the entity that already exists.</param>
        public EntityAlreadyExsistsException(object id) : base($"An entity with ID `{id}` already exists.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAlreadyExsistsException"/> class.
        /// </summary>
        /// <param name="entityName">Entity name, e.g. `User`.</param>
        /// <param name="id">The ID of the entity that already exists.</param>
        public EntityAlreadyExsistsException(string entityName, object id) : base($"{entityName} with ID `{id}` already exists.") { }
    }
}
