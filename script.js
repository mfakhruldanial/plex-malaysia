fetch("/movies.json")
.then(res=>res.json())
.then(data=>{

data.forEach(movie=>{
displayMovie(movie)
})

})