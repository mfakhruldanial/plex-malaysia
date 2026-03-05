const apiKey = "b25c52a6"

async function searchMovie(){

let title = document.getElementById("title").value

let res = await fetch(`https://www.omdbapi.com/?apikey=${apiKey}&t=${title}`)

let movie = await res.json()

document.getElementById("result").innerHTML = `

<img src="${movie.Poster}" width="200">

<h2>${movie.Title}</h2>

<p>${movie.Year}</p>

<button onclick='addMovie(${JSON.stringify(movie)})'>
Add Movie
</button>

`

}

async function addMovie(movie){

await fetch("/api/addMovie",{

method:"POST",
headers:{ "Content-Type":"application/json" },

body: JSON.stringify({

title: movie.Title,
poster: movie.Poster,
year: movie.Year

})

})

alert("Movie added!")

}