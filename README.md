# Elasticsearch_Example
Set up Elasticsearch By NEST Library version 7.10.1 with ASP.NET Core 3.1 and Docker.
to run and test project,you must do the following steps first.
you must first create network to watch Elasticsearch and Kibana on Docker by below commands:

<b>Elasticsearch:</b>

<code>docker run --rm -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" --name elasticsearch --network esnetwork elasticsearch:7.9.3</code>

<b>Kibana:</b>

<code>docker run --rm -p 5601:5601 --name kibana --network esnetwork kibana:7.9.3</code>

after run containers.now We can run and test project.

to run CreateIndex call below url:

HttpGet --> http://localhost:5000/post/createIndex

to run reIndex call below url:

reIndex means :remove current index then create new index and bind data to index.

HttpGet --> http://localhost:5000/post/reIndex

to run deleteDocument call below url:

HttpDelete --> http://localhost:5000 /post/deleteDocument/0f8fad5b-d9cb-469f-a165-70867728950e

to run searchDocumnet call bellow url:

HttpPost --> http://localhost:5000/post/searchDocument

body:{"query":"0c885dd3-7dd9-484b-9b20-3e6552bca144"}

to run addDocument call below url:

HttpPost --> http://localhost:5000/post/addDocument

body:
<div>{
"Title": "title00222",
"Slug":"slug00222",
"Excerpt":"excerpt2222",
"Content":"content2222",
"IsPublished":true, 
"Categories":[
    "Game",
    "Clothing",
    "Food",
    "Art"
],
"Comments":[
{
"Id":1,
"Author":"javad barber",
"Email":"j.barber@yahoo.com",
"Content":"Description about barber Book"
},
{
"Id":3,
"Author":"mehdi shabani",
"Email":"mehdi@outlook.com",
"Content":"Description about Fitness Book"
}
]
}
</div>

to run editDocument call below url:

HttpPost --> http://localhost:5000/post/addDocument

body:
{
"Title": "title00222",
"Slug":"slug00222",
"Excerpt":"excerpt2222",
"Content":"content2222",
"IsPublished":true, 
"Categories":[
    "Game",
    "Clothing",
    "Food",
    "Art"
],
"Comments":[
{
"Id":1,
"Author":"javad barber",
"Email":"j.barber@yahoo.com",
"Content":"Description about barber Book"
},
{
"Id":3,
"Author":"mehdi shabani",
"Email":"mehdi@outlook.com",
"Content":"Description about Fitness Book"
}
]
}
