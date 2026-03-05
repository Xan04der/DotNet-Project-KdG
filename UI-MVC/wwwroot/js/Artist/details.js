document.addEventListener("DOMContentLoaded", function () {
    const updateButton = document.getElementById("updateButtonAge");
    
    if (updateButton) {
        updateButton.addEventListener("click", async function(){
            const artistId = updateButton.getAttribute("data-artist-id");
            const newAge = document.getElementById("ageSelect").value;
            
            if (!newAge || isNaN(newAge) || parseInt(newAge) <= 0 || parseInt(newAge) >= 120) {
                console.error("Invalid age (must be between 0 and 120)");
                alert("Please enter a valid age (must be between 0 and 120)");
            }
            
            try {
                const response = await fetch(`/api/Artists/${artistId}`, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({Age: parseInt(newAge) })
                });
                const data = await response.json();
                console.log(data);
            } catch (error) {
                console.error("Request failed",error);
            }
        })
    }
})