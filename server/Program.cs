var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.UseSession();



app.MapGet("/", () => "Hello World!");

app.MapPost("/api/set-session", (Delegate)SetSession);
app.MapGet("/api/get-session", (Delegate)GetSession);
app.MapDelete("/api/clear-session", (Delegate)ClearSession);

async Task<IResult> SetSession(HttpContext context)
{
    if (context.Session.GetString("email") != null)
    {
        return Results.BadRequest("Session already set");
    }
    Console.WriteLine("SetSession is called..Setting session");
    await Task.Run(() => context.Session.SetString("email", "superadmin@admin.com"));
    return Results.Ok(context.Session.GetString("email"));
}

async Task<IResult> GetSession(HttpContext context)
{
    Console.WriteLine("GetSession is called..Getting session");
    var email = await Task.Run(() => context.Session.GetString("email"));
    if (email == null)
    {
        return Results.NotFound("Session not found");
    }
    return Results.Ok(email);
}

async Task<IResult> ClearSession(HttpContext context)
{
    Console.WriteLine("ClearSession is called..Clearing session");
    await Task.Run(context.Session.Clear);
    return Results.Ok("Session cleared");
}

await app.RunAsync();
