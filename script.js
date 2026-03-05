const catalog = document.getElementById("catalog");

function displayMovies(list){

catalog.innerHTML="";

list.forEach(movie=>{

const div=document.createElement("div");
div.className="movie";

div.innerHTML=`
<img src="${movie.poster}">
`;

div.onclick=()=>{
showPopup(movie);
}

catalog.appendChild(div);

});

}

displayMovies(movies);

function showPopup(movie){

document.getElementById("popup").style.display="block";
document.getElementById("popupPoster").src=movie.poster;
document.getElementById("popupTitle").innerText=movie.title;
document.getElementById("popupInfo").innerText=movie.info;

}

document.getElementById("close").onclick=()=>{
document.getElementById("popup").style.display="none";
}