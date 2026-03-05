const tableBody = document.getElementById("museumsTableBody")
const refreshButton = document.getElementById("refreshButton")

async function fetchMuseums(){
    const response = await fetch("/api/museums")
    if (response.status === 200) {
        tableBody.innerHTML = ' '
        
        const museums = await response.json()
        for (const museum of museums) {
            tableBody.innerHTML += `
            <tr>
            <td>${museum.name}</td>
            <td>${museum.location}</td>
            <td>${museum.yearEstablished}</td>
            </tr>
            `
        }
    }
}

fetchMuseums()

refreshButton.addEventListener("click", fetchMuseums)