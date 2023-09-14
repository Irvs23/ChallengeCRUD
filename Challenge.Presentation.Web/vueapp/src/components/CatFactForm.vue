<template>
    <div>
        <h2>Cat Fact Form</h2>
        <div class="container">
            <form @submit.prevent="getRandomCatFact">
                <button type="submit">Get Random Cat Fact</button>
            </form>
        </div>

        <div v-if="catFact">
            <h3>Random Cat Fact:</h3>
            <p>{{ catFact.fact }}</p>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                catFact: null
            };
        },
        methods: {
            async getRandomCatFact() {
                try {
                    const response = await fetch('/api/CatFact/GetRandomCatFact'); // Ruta de tu API
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const data = await response.body;
                    this.catFact = data;
                } catch (error) {
                    console.error('Error fetching cat fact:', error);
                }
            }
        }
    };
</script>

<style scoped>
    <style scoped >
    .container {
        max-width: 400px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 8px;
        background-color: #f9f9f9;
    }

    h2 {
        text-align: center;
        margin-bottom: 20px;
    }

    form {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    input {
        width: 100%;
        padding: 8px;
        margin-bottom: 10px;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }

    button {
        padding: 10px 20px;
        background-color: #007bff;
        color: #fff;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        font-size: 16px;
    }

        button:hover {
            background-color: #0056b3;
        }

    .result {
        margin-top: 20px;
        text-align: center;
    }

        .result h3 {
            font-size: 20px;
            margin-bottom: 10px;
        }

        .result p {
            font-size: 16px;
        }
</style>
