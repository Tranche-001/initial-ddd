using FluentResults;
using studyRats.Service.Platform.Domain.Entities.Users;
using studyRats.Service.Platform.Domain.ValueObjects;

namespace studyRats.Service.Platform.Domain.Entities.Users
{
    // Doubts about implementation
    // Read https://enterprisecraftsmanship.com/posts/functional-c-primitive-obsession/
    public class User 
    {
        public Guid Id { get; private set;}

        public string Name { get; private set;}

        public Email Email { get; private set;}

        // Private constructor ensures the "Create" method is the only entry point
        private User(Guid id, string name, Email email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static Result<User> Creates(string name, Email email)
        {
            if(name == null)
                throw new ArgumentNullException(nameof(name));
            if(email == null)
                throw new ArgumentNullException(nameof(email));
            
            return Result.Ok(new User(Guid.NewGuid(), name, email));
        }
    }
}



//public static User Create(string name, string email)
//{

//    // If (name does not pass the validations rules) throw new InvalidUserNameException(name);
//    // It should have passed the validation rules before reaching this point,
//    // but we can add an extra layer of safety here to ensure that the invariants are maintained.
//    // That is the reason why we are throwing an exception, because it breaks the assumption that the name is valid,
//    // therefore the object in an invalid state means we introduced a bug in the code,
//    // and we want to catch that as early as possible.
//    // More on that on https://enterprisecraftsmanship.com/posts/always-valid-domain-model/

//    // But because we are expecting that Name and Email already has passed the validation rules,
//    // we can just throw a null exception here,
//    if (name == null) throw new ArgumentNullException(nameof(name));
//    if (email == null) throw new ArgumentNullException(nameof(email));

//    var emailResult = Email.Create(email);


//    return new User(Guid.NewGuid(), name, Email.Create(email).Value);
//}
