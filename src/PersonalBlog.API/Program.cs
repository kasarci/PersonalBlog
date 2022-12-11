using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PersonalBlog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Serialize the GUID as string in the database.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
//Serialize the DateTimeOffset as string in the database.
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
// For initializing the extension class.
builder.Services.Init(builder.Configuration);
builder.Services.AddFluentValidation();
builder.Services.AddDependencyInjections();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddIdentityExtension();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerExtension();

builder.Services.AddAuthenticationAndAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
