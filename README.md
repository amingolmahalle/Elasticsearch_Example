# Elasticsearch_Example
Set up Elasticsearch By NEST Library version 7.10.1 with ASP.NET Core 3.1 and Docker.

to run and test project,you must do the following steps first.

<code>sudo docker create network esnetwork</code>

you must first create network to watch Elasticsearch and Kibana on Docker by below commands:

<h3>Elasticsearch:</h3>

<code>docker run --rm -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" --name elasticsearch --network esnetwork elasticsearch:7.9.3</code>

<h3>Kibana:</h3>

<code>docker run --rm -p 5601:5601 --name kibana --network esnetwork kibana:7.9.3</code>

after run containers,now We can run and test project.

<h2>Comparison between Elasticsearch and RDBMS</h2>

<p>In Elasticsearch, index is similar to tables in RDBMS (Relation Database Management System). Every table is a collection of rows just as every index is a collection of documents in Elasticsearch. </p>

<p>The following table gives a direct comparison between these terms.</p>

<table class="table table-bordered" style="text-align:center;">
<tbody><tr>
<th>Elasticsearch</th>
<th>RDBMS</th>
</tr>
<tr>
<td>Cluster</td>
<td>Database</td>
</tr>
<tr>
<td>Shard</td>
<td>Shard</td>
</tr>
<tr>
<td>Index</td>
<td>Table</td>
</tr>
<tr>
<td>Field</td>
<td>Column</td>
</tr>
<tr>
<td>Document</td>
<td>Row</td>
</tr>
</tbody></table>

<b>to run CreateIndex api, call below url:</b>

HttpGet --> http://localhost:5000/post/createIndex

<b>to run reIndex api, call below url:</b>

reIndex means :remove current index then create new index and bind data to index.

HttpGet --> http://localhost:5000/post/reIndex

<b>to run deleteDocument api, call below url:</b>

HttpDelete --> http://localhost:5000 /post/deleteDocument/0f8fad5b-d9cb-469f-a165-70867728950e

<b>to run searchDocumnet api, call bellow url:</b>

HttpPost --> http://localhost:5000/post/searchDocument

body:{"query":"0c885dd3-7dd9-484b-9b20-3e6552bca144"}

<b>to run addDocument api, call below url:</b>

HttpPost --> http://localhost:5000/post/addDocument

body:
<pre>{
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
</pre>

<b>to run editDocument api, call below url:</b>

HttpPost --> http://localhost:5000/post/editDocument/0f8fad5b-d9cb-469f-a165-70867728950e

body:
<pre>
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
</pre>
