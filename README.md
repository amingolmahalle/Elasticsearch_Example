# Elasticsearch_Example
Set up Elasticsearch By NEST Library version 7.10.1 with ASP.NET Core 3.1 and Docker.

to run and test project,you must do the following steps first.

firstyou have to create network to watch Elasticsearch and Kibana on Docker by below command:

<code>sudo docker create network esnetwork</code>

<h3>Elasticsearch:</h3>

<code>docker run --rm -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" --name elasticsearch --network esnetwork elasticsearch:7.9.3</code>

<h3>Kibana:</h3>

<code>docker run --rm -p 5601:5601 --name kibana --network esnetwork kibana:7.9.3</code>

after run containers,now We can run and test project.

<h2>before runing and testing the project, it is better to talk about Key Concepts of the Elasticsearch</h2>

<h2>Key Concepts</h2>
<p>The key concepts of Elasticsearch are as follows:</p>
<h3>Node</h3>
<p>It refers to a single running instance of Elasticsearch. Single physical and virtual server accommodates multiple nodes depending upon the capabilities of their physical resources like RAM, storage and processing power.</p>
<h3>Cluster</h3>
<p>It is a collection of one or more nodes. Cluster provides collective indexing and search capabilities across all the nodes for entire data.</p>
<h3>Index</h3>
<p>It is a collection of different type of documents and their properties. Index also uses the concept of shards to improve the performance. For example, a set of document contains data of a social networking application.</p>
<h3>Document</h3>
<p>It is a collection of fields in a specific manner defined in JSON format. Every document belongs to a type and resides inside an index. Every document is associated with a unique identifier called the UID.</p>
<h3>Shard</h3>
<p>Indexes are horizontally subdivided into shards. This means each shard contains all the properties of document but contains less number of JSON objects than index. The horizontal separation makes shard an independent node, which can be store in any node. Primary shard is the original horizontal part of an index and then these primary shards are replicated into replica shards.</p>
<h3>Replicas</h3>
<p>Elasticsearch allows a user to create replicas of their indexes and shards. Replication not only helps in increasing the availability of data in case of failure, but also improves the performance of searching by carrying out a parallel search operation in these replicas.</p>

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

reIndex means :remove current index then create new index and bind data to new index.

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

HttpPut --> http://localhost:5000/post/editDocument/0f8fad5b-d9cb-469f-a165-70867728950e

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
