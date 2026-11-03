using Books.Endpoints.Books;
using Books.Endpoints.Users;
using Books.Interfaces;

namespace Books.Extensions;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var routes = app.MapGroup("/api");

        routes
            .MapGroup("")
            .MapEndpoint<CreateUserRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<AuthenticateRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<IsAuthenticatedRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<LogoutRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<GetUsersRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<DeleteUserRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<CreateBookRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<GetBooksRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<GetBookByIdRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<GetBooksByStatusRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<UpdateBookStatusRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<DeleteBookRoute>();

        routes
            .MapGroup("")
            .MapEndpoint<UpdateBookImageRoute>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}