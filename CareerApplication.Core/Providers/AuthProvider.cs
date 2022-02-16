using Firebase.Auth;

namespace CareerApplication.Core.Providers;

public class AuthProvider
{
    private readonly FirebaseAuthProvider _firebaseAuth;

    public AuthProvider(FirebaseAuthProvider firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
    }

    public async Task<FirebaseAuthLink> SignInWithEmailAndPassword(string email, string password)
    {
        return await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
    }

    public async Task<FirebaseAuthLink> CreateUserWithEmailAndPassword(string email, string password)
    {
        return await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);
    }

    public async Task SendPasswordResetEmail(string email)
    {
        await _firebaseAuth.SendPasswordResetEmailAsync(email);
    }
}
