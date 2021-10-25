clean:
	dotnet clean
.PHONY: clean

build:
	dotnet build
.PHONY: build

run:
	dotnet run dotnet run --project src/Api/Api.csproj --launch-profile $(or $(profile), Local)
.PHONY: run

add-migration:
	dotnet ef migrations add $(name) --project src/Infrastructure --output-dir Database/Migrations
.PHONY: add-migration

migrate:
	dotnet ef database update --project src/Infrastructure
.PHONY: migrate

docker:
	docker build -t ${tag} -f Dockerfile .
.PHONY: docker
