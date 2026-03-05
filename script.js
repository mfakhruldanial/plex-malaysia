const catalog = document.getElementById("catalog");
const search = document.getElementById("search");

function displayMovies(list){
catalog.innerHTML="";

list.forEach(movie=>{

const div=document.createElement("div");
div.className="movie";

div.innerHTML=`
<img src="${movie.poster}">
<p>${movie.title}</p>
`;

div.onclick=()=>{
showPopup(movie);
}

catalog.appendChild(div);

});
}

function filterCategory(cat){

if(cat==="all"){
displayMovies(movies);
return;
}

displayMovies(movies.filter(m=>m.category===cat));

}

search.addEventListener("keyup",()=>{

const value=search.value.toLowerCase();

displayMovies(movies.filter(m=>m.title.toLowerCase().includes(value)));

});

function showPopup(movie){

document.getElementById("popup").style.display="block";
document.getElementById("popupPoster").src=movie.poster;
document.getElementById("popupTitle").innerText=movie.title;
document.getElementById("popupInfo").innerText=movie.info;

}

document.getElementById("close").onclick=()=>{
document.getElementById("popup").style.display="none";
}

displayMovies(movies);