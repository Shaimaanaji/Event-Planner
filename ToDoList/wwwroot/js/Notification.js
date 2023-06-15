const oracledb = require('oracledb');

async function getDataFromTable() {
    try {
        await oracledb.createPool({
            user: "SYSTEM",
            password: "system",
            connectString: "OracleConnectionString"
        });

        const query = `SELECT * FROM Notifications`;

        const connection = await oracledb.getConnection();
        const result = await connection.execute(query);

        const rows = result.rows;
        for (let i = 0; i < rows.length; i++) {
            const row = rows[i];
            console.log(row);
        }
        await connection.close();
    } catch (error) {
        console.error(error);
    } finally {
        await oracledb.getPool().close();
    }
}


getDataFromTable();
