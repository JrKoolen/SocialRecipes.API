document.addEventListener("DOMContentLoaded", () => {
    const recipeId = "<%= recipe.id %>"; 
    const commentInput = document.getElementById("comment-input");
    const submitButton = document.getElementById("submit-comment");
    const commentsList = document.getElementById("comments-list");

    const BASE_URL = "<%= process.env.BASE_URL %>"; 

    async function fetchComments() {
      try {
        const response = await fetch(`${BASE_URL}/Comment/ByRecipe/${recipeId}`);
        if (!response.ok) throw new Error("Failed to fetch comments");
        const comments = await response.json();

        commentsList.innerHTML = "";

        comments.forEach(comment => {
          const commentDiv = document.createElement("div");
          commentDiv.className = "comment";
          commentDiv.innerHTML = `
            <p><strong>${comment.username}:</strong> ${comment.text}</p>
            <small>${new Date(comment.timestamp).toLocaleString()}</small>
          `;
          commentsList.appendChild(commentDiv);
        });
      } catch (error) {
        console.error(error);
      }
    }

    async function submitComment() {
      const commentText = commentInput.value.trim();
      if (!commentText) {
        alert("Comment cannot be empty!");
        return;
      }
  
      try {
        const response = await fetch(`${BASE_URL}/Comment/Add`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            recipeId,
            text: commentText
          })
        });
  
        if (!response.ok) throw new Error("Failed to add comment");
        
        commentInput.value = ""; 
        fetchComments(); 
      } catch (error) {
        console.error(error);
        alert("Failed to add comment. Please try again.");
      }
    }
    submitButton.addEventListener("click", submitComment);
    fetchComments();
  });
  