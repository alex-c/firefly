using System;

namespace Firefly.Services.Exceptions
{
    /// <summary>
    /// Indicates that an entity could not be found.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        public EntityNotFoundException() : base("Entity could not be found.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The ID of the entity that could not be found.</param>
        public EntityNotFoundException(object id) : base($"An entity with ID `{id}` could not be found.") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="entityName">Entity name, e.g. `User`.</param>
        /// <param name="id">The ID of the entity that could not be found.</param>
        public EntityNotFoundException(string entityName, object id) : base($"{entityName} `{id}` could not be found.") { }
    }
}
