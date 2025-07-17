using backEnd.Core.Mongo;
using backEnd.Src.Interfaces;
using backEnd.Src.Services;
using backEnd.Utils.Mapper;

var builder = WebApplication.CreateBuilder(args);

//MongoDB Configuration
#region mongoDb
builder.Services.AddSingleton<IMongoDbContext>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var logger = sp.GetRequiredService<ILogger<MongoDbContext>>();

    var connectionString = config.GetValue<string>("MongoDB:ConnectionString")
        ?? throw new ArgumentException("MongoDB connection string is missing");

    var databaseName = config.GetValue<string>("MongoDB:DatabaseName")
        ?? throw new ArgumentException("MongoDB database name is missing");

    var mongoContext = new MongoDbContext(connectionString, databaseName, logger);

    // Initialiser les index (à faire une seule fois)
    mongoContext.InitializeIndexes();

    return mongoContext;
});
#endregion

//Configuration AutoMapper
#region AutoMapper
builder.Services.AddAutoMapper(mc => mc.AddProfile(new MapProfile()));
#endregion

// Enregistrement des services
#region services
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IBadge, BadgeService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IComment, CommentService>();
builder.Services.AddScoped<Reaction, ReactionService>();
builder.Services.AddScoped<IMessage, MessageService>();
builder.Services.AddScoped<INotification, NotificationService>();
builder.Services.AddScoped<IPoll, PollService>();
builder.Services.AddScoped<IPost, PostService>();
builder.Services.AddScoped<IReport, ReportService>();
builder.Services.AddScoped<ITag, TagService>();
builder.Services.AddScoped<IThread, ThreadService>();
builder.Services.AddScoped<IUser, UserService>();
#endregion

//Ajout des controllers
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();